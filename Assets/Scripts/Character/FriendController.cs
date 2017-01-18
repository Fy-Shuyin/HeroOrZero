using UnityEngine;
using System.Collections;

public class FriendController : MonoBehaviour {

	FriendIState FriendState;

	public NavMeshAgent FriendAgent;
	public Animator FriendAnimator;

	public ArrayList AttributeList;
	public GameObject Leader;			//頭領
	public string TargetTag;			//目标类型
	public string Type;					//类型
	public string CharacterName;		//人物名稱
	public GameObject WeaponSound;		//武器声音
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
	public float SightRange;			//追击距离
	public bool IsLive;					//存活

	public ArrayList ActiveSkillSelect;	//选择的主动技能
	public ArrayList PassiveSkillSelect;//选择的被动技能

	public GameObject AttackTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeState(FriendIState nextState)
	{
		FriendState.Exit(this);

		FriendState = nextState;

		FriendState.Enter(this);
	}
}
