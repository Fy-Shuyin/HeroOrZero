using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour 
{
	HeroIState Hero_State;
	FingerEvent FingerEvent;
	CharacterAttribute Attribute;
	public PatternsOfThinking Patterns;

	public NavMeshAgent HeroAgent;
	public Animator HeroAnimator;

	private bool AImode;
	Ray TouchRay;
	Vector3 TouchPosition;

	public string CharacterName;		//人物名稱
	public GameObject WeaponSound;		//武器声音
	public float Experience;			//经验值
	public int LeaderShip;				//统帅力
	public int AttackType;				//攻击方式	true 近		false 远
	public float AttackSpeed;			//攻击速度
	public float AttackRange;			//攻击距离
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
	public bool IsLife;					//存活

	public ArrayList ActiveSkill;		//主动技能
	public ArrayList ActiveSkillSelect;	//选择的主动技能
	public ArrayList PassiveSkill;		//被动技能
	public ArrayList PassiveSkillSelect;//选择的被动技能

	public string[] Friends;			//友軍
	public int[]	FriendsNumber;		//友軍數量

	public GameObject AttackTarget;		//攻击目标
	public string AttackTargetTag;		//目标类型
	public float AttackCooldown;		//攻擊冷卻
	bool isSkill;
	string Type;

	void Start () 
	{
		HeroAgent = GetComponent<NavMeshAgent> ();
		HeroAnimator = GetComponent<Animator> ();

		FingerEvent = new FingerEvent ();
		Patterns = new PatternsOfThinking ();
		Attribute = new CharacterAttribute ();
		Type = gameObject.name.ToUpper();
		Attribute.AttributeInitialize (Type , ref CharacterName , ref WeaponSound , ref AttackType , ref AttackSpeed , ref AttackRange , 
			ref Experience , ref LeaderShip , ref HealthPower , ref HealthPowerAdditional , ref Attack , ref AttackAdditional , 
			ref Defence , ref DefenceAdditional , ref Dexterity , ref DexterityAdditional , ref Hit , ref HitAdditional , ref Agility , ref AgilityAdditional ,
			ref Dodge , ref DodgeAdditional , ref Critical , ref CriticalAdditional , ref MoveSpeed , ref SightRange);
		HeroAgent.speed = MoveSpeed/60f;
		IsLife = true;

		AttackTarget = null;
		AImode = true;

		Hero_State = new HeroIdle ();
		Hero_State.Enter (this);
	}

	void Update () 
	{
		if (AImode == true) 
		{
			Automatic ();
		}
		if (AImode == false) 
		{
			TouchPosition = gameObject.transform.position;
			Manual ();
		}
		Death ();
	}

	public void setAIMode(bool aiMode)
	{
		this.AImode = aiMode;
	}

	void Automatic()
	{
		Hero_State.Execute (this);
	}

	void Manual()
	{
		var putDown = Input.GetKeyDown (KeyCode.Mouse0);
		if (putDown) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			int i = ~(1 << 5);
			if (Physics.Raycast (ray, out hit)) {
				
				Debug.DrawLine (ray.origin, hit.point, Color.red, 2);
				TouchPosition = hit.point;
				Debug.Log (TouchPosition);
			}
			HeroAgent.SetDestination (TouchPosition);
		}
	}

	public void ChangeState(HeroIState nextState)
	{
		Hero_State.Exit(this);

		Hero_State = nextState;

		Hero_State.Enter(this);
	}

	public void Target()
	{
		Collider[] cols = Patterns.Distance(gameObject,SightRange);
		if (cols.Length > 0) 
		{
			AttackTarget = cols[0].gameObject;
			AttackTargetTag = AttackTarget.tag;
		}
	}

	public void runAttack()
	{
		isSkill = false;
		Vector3 point = gameObject.transform.position;
		point.y += 2f;
		var prefab = Resources.Load ("AttackResolution");
		GameObject resolution = Instantiate (prefab) as GameObject;
		resolution.transform.position = point;
		var attackResolution = resolution.GetComponent<AttackResolution> ();
		attackResolution.setAttAttr (isSkill , AttackTargetTag, Attack + AttackAdditional, Hit + HitAdditional, Critical + CriticalAdditional);
		var rigidboby = resolution.GetComponent<Rigidbody>();
		rigidboby.AddForce (gameObject.transform.forward * 200f);
	}

	public void Death()
	{
		if (HealthPower + HealthPowerAdditional <= 0) 
		{
			IsLife = false;
			gameObject.GetComponent<Collider> ().enabled = false;
		}
	}
}
