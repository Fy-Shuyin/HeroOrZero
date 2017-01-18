using UnityEngine;
using System.Collections;
using System.IO;
using Mono.Data.Sqlite;

public class GameSystem
{
	private DBAccess sql;
    SqliteDataReader reader;

	public void BattleStart(int stageLevel , ref ArrayList heroList , ref ArrayList enemyList)
	{
		if (stageLevel > 15) 
		{
			//All Clear
		}
		Debug.Log ("stage - " + stageLevel);
		if (stageLevel == 1 || stageLevel == 6 || stageLevel == 11) 
		{
			heroList = Heros (stageLevel);
		}
		enemyList = Enemy (stageLevel);
	}

	ArrayList Heros(int num)
	{
		ArrayList heroList = new ArrayList ();
		sql = new DBAccess("data source = HeroOrZero.db");
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

	ArrayList Enemy(int num)
	{
		ArrayList enemyList = new ArrayList ();
		sql = new DBAccess("data source = HeroOrZero.db");
		reader = sql.ReadOneTable("StageAttribute", new string[] { "ID" }, new string[] { "==" }, new string[] { num.ToString() });
		while (reader.Read ()) 
		{
			if(!reader.IsDBNull(reader.GetOrdinal("EnemyOne")))
				enemyList.Add (EnemySet(reader.GetString(reader.GetOrdinal("EnemyOne")) , reader.GetInt32(reader.GetOrdinal("EnemyOneNum"))));
			if(!reader.IsDBNull(reader.GetOrdinal("EnemyTwo")))
				enemyList.Add (EnemySet(reader.GetString(reader.GetOrdinal("EnemyTwo")) , reader.GetInt32(reader.GetOrdinal("EnemyTwoNum"))));
			if(!reader.IsDBNull(reader.GetOrdinal("EnemyThree")))
				enemyList.Add (EnemySet(reader.GetString(reader.GetOrdinal("EnemyThree")) , reader.GetInt32(reader.GetOrdinal("EnemyThreeNum"))));
		}
		sql.CloseConnection ();
		return enemyList;
	}

	ArrayList EnemySet(string name , int num)
	{
		ArrayList list = new ArrayList ();
		if (name != "" && num != 0) 
		{ 	
			list.Add (name);
			list.Add (num);
		}
		return list;
	}

	public void ChangeData(string table , string key , string keyValue , string type , string value)
	{
		sql = new DBAccess("data source = HeroOrZero.db");
		sql.UpdateValues(table, new string[]{ type }, new string[]{"'" + value + "'"}, key , "=", "'" + keyValue +"'");
		sql.CloseConnection ();
	}
}
