using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	EnemyIState Enemy_State;
	CharacterAttribute Attribute;
	public PatternsOfAttribute Patterns;

	public NavMeshAgent EnemyAgent;
	public Animation EnemyAnimator;

	public GameObject TargetEGG;		//攻击目标
	public string TargetTag;			//目标类型
	public string Type;					//怪物类型
	public string CharacterName;		//人物名稱
	public string CharacterType;		//人物性格
	public string WeaponSound;		//武器声音
	public int AttackType;				//攻击方式	true 近		false 远
	public float AttackSpeed;			//攻击速度
	public float AttackCooldown;		//攻击冷却
	public float AttackRange;			//攻击距离
	public int HealthPowerMax;			//最大生命值
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
	public bool IsLive;					//存活

	public ArrayList ActiveSkillSelect;	//选择的主动技能
	public ArrayList PassiveSkillSelect;//选择的被动技能

	public int Command;
	public GameObject AttackTarget;
	bool isSkill;

	void Start () 
	{
		EnemyAgent = GetComponent<NavMeshAgent> ();
		EnemyAnimator = GetComponent<Animation> ();

		Patterns = new PatternsOfAttribute ();
		Attribute = new CharacterAttribute ();
		Type = gameObject.name.Replace("(Clone)","");
		Attribute.AttributeInitialize (Type , ref CharacterName , ref WeaponSound , ref AttackType , ref AttackSpeed , ref AttackRange , ref HealthPower , ref HealthPowerAdditional ,
			ref Attack , ref AttackAdditional , ref Defence , ref DefenceAdditional , ref Dexterity , ref DexterityAdditional , ref Hit , ref HitAdditional , ref Agility , ref AgilityAdditional ,
			ref Dodge , ref DodgeAdditional , ref Critical , ref CriticalAdditional , ref MoveSpeed , ref FieldOfVision , ref SightRange);
		EnemyAgent.speed = MoveSpeed/60f;
		IsLive = true;

		TargetEGG = GameObject.Find ("TownHall");
		AttackTarget = TargetEGG;

		//var prefab = Resources.Load<GameObject> ("UI/CharacterHpBar");
		//var hpBarObject = GameObject.Instantiate<GameObject> (prefab);
		//var hpBar = hpBarObject.GetComponent<CharacterHpBar> ();
		//hpBar.Initialize (gameObject);

		Enemy_State = new EnemyMove ();
		Enemy_State.Enter (this);
	}
	
	void Update () 
	{
		Enemy_State.Execute (this);
		Death ();
	}

	public void ChangeState(EnemyIState nextState)
	{
		Enemy_State.Exit(this);

		Enemy_State = nextState;

		Enemy_State.Enter(this);
	}

	public void Move(Vector3 target)
	{
		EnemyAgent.Resume();
		EnemyAgent.SetDestination(target);
	}

	public void Target()
	{
		Collider[] cols = Patterns.Distance(gameObject,SightRange);
		if (cols.Length > 0)
		{
			AttackTarget = cols[0].gameObject;
			TargetTag = AttackTarget.tag;
		}
		else
		{
			AttackTarget = TargetEGG;
			TargetTag = TargetEGG.tag;
		}
	}

	public void runAttack()
	{
		isSkill = false;
		Vector3 point = gameObject.transform.position;
		point.y += 1.5f;
		var prefab = Resources.Load ("AttackResolution");
		GameObject resolution = Instantiate (prefab) as GameObject;
		resolution.transform.position = point;
		var attackResolution = resolution.GetComponent<AttackResolution> ();
		attackResolution.setAttAttr (TargetTag, gameObject.transform.position , AttackTarget , AttackType , AttackSpeed ,Attack + AttackAdditional, Hit + HitAdditional, Critical + CriticalAdditional);
	}

	void Death()
	{
		if (HealthPower + HealthPowerAdditional <= 0) 
		{
			gameObject.GetComponent<Collider> ().enabled = false;
			Destroy (gameObject, 2f);
			IsLive = false;
		}
	}
}
