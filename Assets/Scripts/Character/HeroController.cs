using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour 
{
	HeroIState Hero_State;
	FingerEvent FingerEvent;
	CharacterAttribute Attribute;
	public PatternsOfAttribute Patterns;
	public PatternsOfCharacter Character;
	public PatternsOfSkills Skills;

	public NavMeshAgent HeroAgent;
	public Animator HeroAnimator;

	public bool AImode;
	private Ray TouchRay;
	public Vector3 TouchPosition;

	public string CharacterName;		//人物名稱
	public string CharacterType;		//人物性格
	public string WeaponSound;			//武器声音
	public float Experience;			//经验值
	public int LeaderShip;				//统帅力
	public int AttackType;				//攻击方式	0 近		1 远
	public float AttackSpeed;			//攻击速度
	public float AttackRange;			//攻击距离
	public float AttackAngle;			//攻击角度
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
	public float StopTime;				//停止事件

	public string SpellSkillName;
	public ArrayList ActiveSkill;		//主动技能
	public ArrayList ActiveSkillSelect;	//选择的主动技能
	public ArrayList PassiveSkill;		//被动技能
	public ArrayList PassiveSkillSelect;//选择的被动技能

	public string[] Friends;			//友軍
	public int[]	FriendsNumber;		//友軍數量
	public int 		Command;			//命令

	private Collider HeroCollider;
	public Vector3 MovePoint;			//移动地点
	public GameObject AttackTarget;		//攻击目标
	private GameObject AttackTargetEffect;//攻击目标的显示的特效
	public string AttackTargetTag;		//目标类型
	public float AttackCooldown;		//攻擊冷卻
	private string Type;				//人物职业

	void Start () 
	{
		HeroAgent = GetComponent<NavMeshAgent> ();
		HeroAnimator = GetComponent<Animator> ();

		FingerEvent = new FingerEvent ();

		Attribute = new CharacterAttribute ();
		Patterns = new PatternsOfAttribute ();
		Character = new PatternsOfCharacter ();
		Skills = new PatternsOfSkills ();

		IsLive = true;
		Type = gameObject.name.Replace("(Clone)","");
		Attribute.AttributeInitialize (Type , ref CharacterName , ref WeaponSound , ref AttackType , ref AttackSpeed , ref AttackRange , 
			ref Experience , ref LeaderShip , ref HealthPower , ref HealthPowerAdditional , ref Attack , ref AttackAdditional , 
			ref Defence , ref DefenceAdditional , ref Dexterity , ref DexterityAdditional , ref Hit , ref HitAdditional , ref Agility , ref AgilityAdditional ,
			ref Dodge , ref DodgeAdditional , ref Critical , ref CriticalAdditional , ref MoveSpeed , ref FieldOfVision , ref SightRange);
		HealthPowerMax = HealthPower + HealthPowerAdditional;
		CharacterType = Character.CharacterType (gameObject, Random.Range(1,10));
		HeroAgent.speed = MoveSpeed/60f;
		Attribute.SkillsInitialize (Type , ref ActiveSkill , ref ActiveSkillSelect , ref PassiveSkill , ref PassiveSkillSelect);
		ActiveSkillSelect = ActiveSkill;
		PassiveSkillSelect = PassiveSkill;
		Skills.setSkills (gameObject, ActiveSkillSelect);
		Skills.setSkills (gameObject, PassiveSkillSelect);

		HeroCollider = gameObject.GetComponent<Collider> ();
		AttackTarget = null;
		AImode = true;

		//var prefab = Resources.Load<GameObject> ("UI/CharacterHpBar");
		//var hpBarObject = GameObject.Instantiate<GameObject> (prefab);
		//var hpBar = hpBarObject.GetComponent<CharacterHpBar> ();
		//hpBar.Initialize (gameObject);

		Hero_State = new HeroIdle ();
		Hero_State.Enter (this);
	}

	void Update () 
	{
		Automatic ();
		if (!AImode) 
		{
			Manual ();
		}
		else 
		{
			if (AttackTargetEffect != null) 
			{
				Destroy(AttackTargetEffect.transform.FindChild ("AttactTarget(Clone)").gameObject);
				AttackTargetEffect = null;
			}
		}
		Death ();
	}
	//モードを設定する
	public void setAIMode(bool aiMode)
	{
		this.AImode = aiMode;
	}
	//自動モード
	void Automatic()
	{
		Hero_State.Execute (this);
	}
	//操作モード
	void Manual()
	{
		if (!Hero_State.ToString ().Equals ("HeroStop")) 
		{
			FingerEvent. ClickPoint (gameObject);
		}

		if (AttackTarget != null && AttackTarget != AttackTargetEffect) 
		{
			if (AttackTargetEffect != null) 
			{
				Destroy(AttackTargetEffect.transform.FindChild ("AttactTarget(Clone)").gameObject);
			}
			AttackTargetEffect = AttackTarget;
			var prefab = Resources.Load ("AttackEffect/AttactTarget");
			GameObject effect = Instantiate (prefab) as GameObject;
			effect.transform.position = AttackTarget.transform.position;
			effect.transform.SetParent(AttackTarget.transform);
			effect.transform.localScale = new Vector3 (1,1,1);
		}
	}

	public void ChangeState(HeroIState nextState)
	{
		Hero_State.Exit(this);

		Hero_State = nextState;

		Hero_State.Enter(this);
	}

	public void ChangeToMoveStage(Vector3 point)
	{
		point.y = 0;
		TouchPosition = point;
		if (!Hero_State.ToString().Equals("HeroMove")) 
		{
			ChangeState (new HeroMove ());
		}
	}
	public void ChangeToSpellSkillStage(GameObject target, bool isChange, string skillName)
	{
		if (isChange) {
			AttackTarget = target;
			AttackTargetTag = target.tag;
		}
		SpellSkillName = skillName;
		ChangeState (new HeroSpellSkills ());
	}
	public void ChangeToStopStage(float stopTime)
	{
		StopTime = stopTime;
		ChangeState (new HeroStop ());
	}

	public void Target()
	{
		Collider[] cols = Patterns.DistanceList(gameObject,FieldOfVision);
		if (cols.Length > 0) 
		{
			AttackTarget = cols[0].gameObject;
			AttackTargetTag = AttackTarget.tag;
		}
	}

	public void Target(Transform target)
	{
		AttackTarget = target.gameObject;
		AttackTargetTag = target.tag;
	}

	public void movePoint(Vector3 point)
	{
		HeroAgent.SetDestination (point);
	}

	public bool Think()
	{
		return Character.runThinkType (gameObject, CharacterType);
	}

	public void runAttack()
	{
		Vector3 point = gameObject.transform.position;

		if (AttackType == 0) 
		{
			point.y += HeroCollider.bounds.size.y/2;
			var prefab = Resources.Load ("AttackEffect/AttackResolution");
			GameObject resolution = Instantiate (prefab) as GameObject;
			resolution.transform.position = point;
			var attackResolution = resolution.GetComponent<AttackResolution> ();
			attackResolution.setAttAttr (gameObject, AttackTarget, AttackType, AttackSpeed, AttackAngle ,Attack + AttackAdditional, Hit + HitAdditional, Critical + CriticalAdditional);
		}
		if (AttackType == 1) 
		{
			//攻击模型数据库待添加
			point.x -= HeroCollider.bounds.size.x * gameObject.transform.forward.normalized.x;
			point.y += HeroCollider.bounds.size.y;
			point.z -= HeroCollider.bounds.size.z * gameObject.transform.forward.normalized.z;
			var prefab = Resources.Load ("AttackEffect/MageAttack");
			GameObject resolution = Instantiate (prefab) as GameObject;
			resolution.transform.position = point;
			var attackResolution = resolution.AddComponent<AttackResolution> ();
			attackResolution.setAttAttr (gameObject, AttackTarget, AttackType, AttackSpeed, AttackAngle ,Attack + AttackAdditional, Hit + HitAdditional, Critical + CriticalAdditional);
		}
	}

	public void Death()
	{
		if (HealthPower + HealthPowerAdditional <= 0) 
		{
			IsLive = false;
			AImode = true;
			gameObject.GetComponent<Collider> ().enabled = false;
		}
	}
}
