using UnityEngine;
using System.Collections;
using System.IO;
using Mono.Data.Sqlite;

public class GameSystem
{
	private DBAccess sql;
    SqliteDataReader reader;

	public void BattleStart(int stageLevel , ref ArrayList heroList , ref Hashtable enemyTable)
	{
		if (stageLevel > 15) 
		{
			//All Clear
		}
		Debug.Log ("stage - " + stageLevel);
		//if (stageLevel == 1 || stageLevel == 6 || stageLevel == 11) 
		//{
			heroList = Heros (stageLevel);
		//}
		enemyTable = Enemy (stageLevel);
	}

	ArrayList Heros(int num)
	{
		ArrayList heroList = new ArrayList ();
		string appDBPath = Application.dataPath + "/HeroOrZero.db";
		sql = new DBAccess("Data Source = " + appDBPath);
		reader = sql.ReadOneTable("StageAttribute", new string[] { "ID" }, new string[] { "==" }, new string[] { num.ToString() });
		while (reader.Read ()) 
		{
			if(!reader.IsDBNull(reader.GetOrdinal("HeroOne")))
				heroList.Add (reader.GetString(reader.GetOrdinal("HeroOne")));
			if(!reader.IsDBNull(reader.GetOrdinal("HeroTwo")))
				heroList.Add (reader.GetString(reader.GetOrdinal("HeroTwo")));
			if(!reader.IsDBNull(reader.GetOrdinal("HeroThree")))
				heroList.Add (reader.GetString(reader.GetOrdinal("HeroThree")));
			
		}
		sql.CloseConnection ();
		return heroList;
	}

	Hashtable Enemy(int num)
	{
		Hashtable enemyTable = new Hashtable ();
		string appDBPath = Application.dataPath + "/HeroOrZero.db";
		sql = new DBAccess("Data Source = " + appDBPath);
		reader = sql.ReadOneTable("StageAttribute", new string[] { "ID" }, new string[] { "==" }, new string[] { num.ToString() });
		while (reader.Read ()) 
		{
			if (!reader.IsDBNull (reader.GetOrdinal ("EnemyOne")))
				enemyTable.Add (reader.GetString (reader.GetOrdinal ("EnemyOne")), reader.GetInt32 (reader.GetOrdinal ("EnemyOneNum")));

			if (!reader.IsDBNull (reader.GetOrdinal ("EnemyTwo")))
				enemyTable.Add (reader.GetString (reader.GetOrdinal ("EnemyTwo")), reader.GetInt32 (reader.GetOrdinal ("EnemyTwoNum")));

			if (!reader.IsDBNull (reader.GetOrdinal ("EnemyThree")))
				enemyTable.Add (reader.GetString (reader.GetOrdinal ("EnemyThree")), reader.GetInt32 (reader.GetOrdinal ("EnemyThreeNum")));
		}
		sql.CloseConnection ();
		return enemyTable;
	}
	/// <summary>
	/// 修改数据
	/// </summary>
	/// <param name="table">数据表</param>
	/// <param name="key">名称</param>
	/// <param name="keyValue">名称的值</param>
	/// <param name="type">关键</param>
	/// <param name="value">新的值</param>
	public void ChangeData(string table , string key , string keyValue , string type , string value)
	{
		string appDBPath = Application.dataPath + "/HeroOrZero.db";
		sql = new DBAccess("Data Source = " + appDBPath);
		sql.UpdateValues(table, new string[]{ type }, new string[]{"'" + value + "'"}, key , "=", "'" + keyValue +"'");
		sql.CloseConnection ();
	}

	public void DeleteData(string table , string key , string keyValue)
	{
		string appDBPath = Application.dataPath + "/HeroOrZero.db";
		sql = new DBAccess("Data Source = " + appDBPath);
		sql.DeleteValuesAND(table, new string[]{ key }, new string[]{"="}, new string[]{ "'" + keyValue + "'"});
		sql.CloseConnection ();
	}
}
