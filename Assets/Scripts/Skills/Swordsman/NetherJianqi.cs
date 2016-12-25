using UnityEngine;
using System.Collections;

public class NetherJianqi : MonoBehaviour {

	FingerEvent finger = new FingerEvent ();
	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	private GameObject Target;
	private string TargetTag;
	private bool TargetSelect;
	private int TargetLayer;

	//DataBase
	public int Level;
	public string SkilType;
	public int SkillMethod;
	private string Effect;
	private float EffectTime;
	private int   AttackType;			//是否飛行
	private float SpellRange;			//飞行距离
	private float Speed;				//飛行速度
	private float LifeTime;				//存在时间
	private float Attack;				//攻击力
	private float AttackLevel;			//攻击力增长
	private float RealAttack;			//实际攻击力
	private float Hit;					//命中率
	private float Critical;				//暴击率
	private float Cooldown;				//冷却时间
	private float CooldownCount;

	void Start () 
	{
		if (gameObject.tag.Equals ("Ally")) 
		{
			TargetLayer = 1 << LayerMask.NameToLayer ("Enemy");
			TargetTag = "Enemy";
		} 
		else if (gameObject.tag.Equals ("Enemy")) 
		{
			TargetLayer = 1 << LayerMask.NameToLayer ("Ally");
			TargetTag = "Ally";
		}
		skllInitialize ();
	}
	
	void Update () 
	{
		if (!isTrigger) 
		{
			if (isSpell) 
			{
				if (patterns.Distance (gameObject, SpellRange) != null) 
				{
					Target = patterns.Distance (gameObject, SpellRange) [0].gameObject;
					if (Target != null) 
					{
						skillStatus ();
						isTrigger = true;
						isSpell = false;
						CooldownCount = Cooldown;
					}
				}
			}	
		} 
		else
		{
			if (CooldownCount == 0) 
			{
				isTrigger = false;
			} 
			else 
			{
				CooldownCount -= Time.deltaTime;
				if (CooldownCount < 0) 
				{
					CooldownCount = 0;
				}
			}
		}
	}

	void skillStatus()
	{
		TriggerPoint = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5f);
		RealAttack = Attack + AttackLevel * Level;
		Hit = patterns.getHit(gameObject);
		Critical = patterns.getCritical(gameObject);

		Debug.Log ("att-" + RealAttack + "hit-" + Hit + "cri-" + Critical);
		var prefab = Resources.Load ("Skills/NetherJianqi");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		var attRes = effect.AddComponent<AttackResolution> ();
		attRes.setSkillAttr ( TargetTag, TriggerPoint, Target, Effect, EffectTime, AttackType, Speed, Attack, Hit, Critical);
	}

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkilType = "Attack";
		SkillMethod = 1;
		Effect = "";
		EffectTime = 0;
		LifeTime = 3f;
		AttackType = 1;		
		SpellRange = 20f;			
		Speed = 20f;
		LifeTime = 3f;			
		Attack = 75;
		AttackLevel = 30;
		Cooldown = 7f;
		TriggerPoint = new Vector3 ();
		Target = null;
	}
}
