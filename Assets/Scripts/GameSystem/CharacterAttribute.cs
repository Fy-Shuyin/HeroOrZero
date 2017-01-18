using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class CharacterAttribute {

	private DBAccess sql;
	SqliteDataReader reader;

	/// <summary>
	/// Attribute
	/// </summary>
	public string CharacterName;		//人物名稱
	public GameObject WeaponSound;		//武器声音
	public bool AttackType;				//攻击方式	true 近		false 远
	public float AttackSpeed;			//攻击速度
	public float AttackRange;			//攻击距离
	public float Experience;			//经验值
	public int LeaderShip;				//统帅力
	public int HealthPower;				//生命值
	public int HealthPowerAdditional;	//追加生命值
	public float Attack;				//物理攻击力
	public float AttackAdditional;		//追加物理攻击力
	public float Defence;				//防御力
	public float DefenceAdditional;		//追加防御力
	public float Dexterity;				//灵巧(影响命中率)
	public float DexterityAdditional;	//追加灵巧
	public float Hit;					//命中率
	public float HitAdditional;			//追加命中率
	public float Agility;				//敏捷(影响回避率)
	public float AgilityAdditional;		//追加敏捷
	public float Dodge;					//回避率
	public float DodgeAdditional;		//追加回避率
	public float Critical;				//暴击率
	public float CriticalAdditional;	//追加暴击率
	public float MoveSpeed;				//移动速度
	public float FieldOfVision;			//视野范围
	public float SightRange;			//追击距离

	public ArrayList ActiveSkill;		//主动技能
	public ArrayList ActiveSkillSelect;	//选择的主动技能
	public ArrayList PassiveSkill;		//被动技能
	public ArrayList PassiveSkillSelect;//选择的被动技能

	public string[] Friends;			//友軍
	public int[]	FriendsNumber;		//友軍數量

	/// <summary>
	/// HeroAttributeInitialize
	/// </summary>
	public void AttributeInitialize(string type , ref string charcterName , ref string weaponSound ,ref int attackType , ref float attackSpeed , ref float attackRange ,
		ref float experience , ref int leaderShip , ref int healthPower , ref int healthPowerAdditional , ref float attack , ref float attackAdditional ,
		ref float defence , ref float defenceAdditional , ref float dexterity , ref float dexterityAdditional , ref float hit , ref float hitAdditional , ref float agility , ref float agilityAdditional ,
		ref float dodge , ref float dodgeAdditional , ref float critical , ref float criticalAdditional , ref float moveSpeed , ref float fieldOfVision , ref float sightRange)
	{
		sql = new DBAccess("data source = HeroOrZero.db");
		reader = sql.ReadOneTable("Heros", new string[] { "ObjectName" }, new string[] { "==" }, new string[] {"'" + type + "'"});
		while (reader.Read ()) 
		{
			charcterName = reader.GetString(reader.GetOrdinal("CharacterName"));	
			//weaponSound = reader.GetString(reader.GetOrdinal("WeaponSound"));				
			experience	= reader.GetInt32(reader.GetOrdinal("Experience"));
			leaderShip	= reader.GetInt32(reader.GetOrdinal("LeaderShip"));
			attackType 	= reader.GetInt32(reader.GetOrdinal("AttackType"));				
			attackSpeed	= reader.GetFloat(reader.GetOrdinal("AttackSpeed"));				
			attackRange = reader.GetFloat(reader.GetOrdinal("AttackRange"));	
			healthPower = reader.GetInt32(reader.GetOrdinal("HealthPower"));		
			healthPowerAdditional = 0;
			attack = reader.GetFloat(reader.GetOrdinal("Attack"));		
			attackAdditional = 0;		
			defence = reader.GetFloat(reader.GetOrdinal("Defence"));	
			defenceAdditional = 0;		
			dexterity = reader.GetFloat(reader.GetOrdinal("Dexterity"));	
			dexterityAdditional = 0;	
			agility = reader.GetFloat(reader.GetOrdinal("Agility"));	
			agilityAdditional = 0;		
			hit = 50 + dexterity;					
			hitAdditional = 0;
			dodge = agility/5;					
			dodgeAdditional = 0;		
			critical = (dexterity + agility)/3 ;				
			criticalAdditional = 0;
			moveSpeed = reader.GetFloat(reader.GetOrdinal("MoveSpeed"));	
			fieldOfVision = reader.GetFloat(reader.GetOrdinal("FieldOfVision"));	
			sightRange = reader.GetFloat(reader.GetOrdinal("SightRange"));	
		}					
		sql.CloseConnection();
	}


	/// <summary>
	/// HeroSkillsInitialize
	/// </summary>
	public void SkillsInitialize(string type , ref ArrayList activeSkill , ref ArrayList activeSkillSelect , ref ArrayList passiveSkill , ref ArrayList passiveSkillSelect)
	{
		activeSkill = new ArrayList ();
		activeSkillSelect = new ArrayList ();
		passiveSkill = new ArrayList ();
		passiveSkillSelect = new ArrayList ();
		sql = new DBAccess("data source = HeroOrZero.db");
		reader = sql.ReadOneTable("HeroSkills", new string[] { "ObjectName" }, new string[] { "==" }, new string[] {"'" + type + "'"});
		while (reader.Read ()) 
		{
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill1")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill1")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill2")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill2")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill3")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill3")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill4")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill4")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill5")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill5")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill6")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill6")));

			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill1")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill1")));
			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill2")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill2")));
			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill3")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill3")));
			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill4")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill4")));
			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill5")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill5")));
			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill6")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill6")));
		}
		sql.CloseConnection ();
	}

	/// <summary>
	/// EnemyAttributeInitialize
	/// </summary>
	public void AttributeInitialize(string type , ref string charcterName , ref string weaponSound , 
		ref int attackType , ref float attackSpeed , ref float attackRange , ref int healthPower , ref int healthPowerAdditional , ref float attack , ref float attackAdditional ,
		ref float defence , ref float defenceAdditional , ref float dexterity , ref float dexterityAdditional , ref float hit , ref float hitAdditional , ref float agility , ref float agilityAdditional ,
		ref float dodge , ref float dodgeAdditional , ref float critical , ref float criticalAdditional , ref float moveSpeed , ref float fieldOfVision , ref float sightRange)
	{
		sql = new DBAccess("data source = HeroOrZero.db");
		reader = sql.ReadOneTable("Enemies", new string[] { "ObjectName" }, new string[] { "==" }, new string[] {"'" + type +"'"});
		while (reader.Read ()) 
		{
			if(!reader.IsDBNull(reader.GetOrdinal("CharacterName")))
				charcterName = reader.GetString(reader.GetOrdinal("CharacterName"));	
			//weaponSound = reader.GetString(reader.GetOrdinal("WeaponSound"));					
			attackType 	= reader.GetInt32(reader.GetOrdinal("AttackType"));				
			attackSpeed	= reader.GetFloat(reader.GetOrdinal("AttackSpeed"));				
			attackRange = reader.GetFloat(reader.GetOrdinal("AttackRange"));	
			healthPower = reader.GetInt32(reader.GetOrdinal("HealthPower"));		
			healthPowerAdditional = 0;
			attack = reader.GetFloat(reader.GetOrdinal("Attack"));		
			attackAdditional = 0;		
			defence = reader.GetFloat(reader.GetOrdinal("Defence"));	
			defenceAdditional = 0;		
			dexterity = reader.GetFloat(reader.GetOrdinal("Dexterity"));	
			dexterityAdditional = 0;	
			agility = reader.GetFloat(reader.GetOrdinal("Agility"));	
			agilityAdditional = 0;		
			hit = 50 + dexterity;					
			hitAdditional = 0;
			dodge = agility/5;					
			dodgeAdditional = 0;		
			critical = (dexterity + agility)/3 ;				
			criticalAdditional = 0;
			moveSpeed = reader.GetFloat(reader.GetOrdinal("MoveSpeed"));	
			fieldOfVision = reader.GetFloat(reader.GetOrdinal("FieldOfVision"));	
			sightRange = reader.GetFloat(reader.GetOrdinal("SightRange"));	
		}					
		sql.CloseConnection();
	}

	/// <summary>
	/// EnemySkillInitialize
	/// </summary>
	public void SkillsInitialize(string type , ref ArrayList activeSkill , ref ArrayList passiveSkill)
	{
		activeSkill = new ArrayList();
		passiveSkill = new ArrayList();
		sql = new DBAccess("data source = HeroOrZero.db");
		reader = sql.ReadOneTable("EnemySkills", new string[] { "ObjectName" }, new string[] { "==" }, new string[] {"'" + type + "'"});
		while (reader.Read ()) 
		{
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill1")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill1")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill2")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill2")));
			if(!reader.IsDBNull(reader.GetOrdinal("ActiveSkill3")))
				activeSkill.Add (reader.GetString(reader.GetOrdinal("ActiveSkill3")));

			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill1")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill1")));
			if(!reader.IsDBNull(reader.GetOrdinal("PassiveSkill2")))
				passiveSkill.Add (reader.GetString(reader.GetOrdinal("PassiveSkill2")));
		}
		sql.CloseConnection ();
	}
}
