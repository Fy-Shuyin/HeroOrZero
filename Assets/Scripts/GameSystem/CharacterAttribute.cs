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
	public void AttributeInitialize(string type , ref string charcterName , ref GameObject weaponSound ,ref int attackType , ref float attackSpeed , ref float attackRange ,
		ref float experience , ref int leaderShip , ref int healthPower , ref int healthPowerAdditional , ref float attack , ref float attackAdditional ,
		ref float defence , ref float defenceAdditional , ref float dexterity , ref float dexterityAdditional , ref float hit , ref float hitAdditional , ref float agility , ref float agilityAdditional ,
		ref float dodge , ref float dodgeAdditional , ref float critical , ref float criticalAdditional , ref float moveSpeed , ref float sightRange)
	{
		//sql = new DBAccess("data source = GameSystem.db");
		//reader = sql.ReadOneTable(tag, new string[] { "TYPE" }, new string[] { "==" }, new string[] {"'WOLF'"});
		charcterName = "オリン";	
		//weaponSound = "";				
		attackType 	= 0;				
		attackSpeed	= 2.2f;			
		attackRange = 4f;			
		experience	= 0;				
		leaderShip	= 10;			
		healthPower = 550;			
		healthPowerAdditional = 0;	
		attack = 40;					
		attackAdditional = 0;		
		defence = 22;				
		defenceAdditional = 0;		
		dexterity = 22;				
		dexterityAdditional = 0;	
		hit = 50 + dexterity;					
		hitAdditional = 0;			
		agility = 15;				
		agilityAdditional = 0;		
		dodge = agility/5;					
		dodgeAdditional = 0;		
		critical = (dexterity + agility)/3 ;				
		criticalAdditional = 0;		
		moveSpeed = 320;				
		sightRange = 20;							
	}

	/// <summary>
	/// EnemyAttributeInitialize
	/// </summary>
	public void AttributeInitialize(string type , ref string charcterName , ref GameObject weaponSound , 
		ref int attackType , ref float attackSpeed , ref float attackRange , ref int healthPower , ref int healthPowerAdditional , ref float attack , ref float attackAdditional ,
		ref float defence , ref float defenceAdditional , ref float dexterity , ref float dexterityAdditional , ref float hit , ref float hitAdditional , ref float agility , ref float agilityAdditional ,
		ref float dodge , ref float dodgeAdditional , ref float critical , ref float criticalAdditional , ref float moveSpeed , ref float sightRange)
	{
		//sql = new DBAccess("data source = GameSystem.db");
		//reader = sql.ReadOneTable(tag, new string[] { "TYPE" }, new string[] { "==" }, new string[] {"'WOLF'"});
		charcterName = "Wolf";	
		//weaponSound = "";					
		attackType 	= 0;				
		attackSpeed	= 2.2f;			
		attackRange = 4f;			
		healthPower = 300;			
		healthPowerAdditional = 0;	
		attack = 30;					
		attackAdditional = 0;		
		defence = 12;				
		defenceAdditional = 0;		
		dexterity = 20;				
		dexterityAdditional = 0;	
		hit = 50 + dexterity;					
		hitAdditional = 0;			
		agility = 10;				
		agilityAdditional = 0;		
		dodge = agility/5;					
		dodgeAdditional = 0;		
		critical = (dexterity + agility)/3 ;				
		criticalAdditional = 0;		
		moveSpeed = 320;				
		sightRange = 20;							
	}

	/// <summary>
	/// EnemySkillInitialize
	/// </summary>
	public void SkillsInitialize(ArrayList activeSkillSelect , ArrayList passiveSkillSelect)
	{
		activeSkillSelect = new ArrayList();
		activeSkillSelect = null;
		passiveSkillSelect = new ArrayList();
		passiveSkillSelect = null;
	}

	/// <summary>
	/// HeroSkillsInitialize
	/// </summary>
	public void SkillsInitialize(ArrayList activeSkill , ArrayList activeSkillSelect , ArrayList passiveSkill , ArrayList passiveSkillSelect)
	{
		activeSkill = new ArrayList ();
		activeSkill [0] = "NetherJianqi";
		activeSkillSelect = new ArrayList ();
		activeSkillSelect [0] = "NetherJianqi";
		passiveSkill = new ArrayList ();
		passiveSkill = null;
		passiveSkillSelect = new ArrayList ();
		passiveSkillSelect = null;			
	}
}
