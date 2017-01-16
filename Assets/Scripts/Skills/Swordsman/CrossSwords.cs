using UnityEngine;
using System.Collections;

public class CrossSwords : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	public GameObject Target;
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
		if (isSpell) 
		{
			skillStatus ();
			isTrigger = true;
			isSpell = false;
			CooldownCount = Cooldown;
		}	

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

	void skillStatus()
	{
		Collider collider = gameObject.GetComponent<Collider> ();
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y += collider.bounds.size.y/2;
		TriggerPoint.z += 5f;
		RealAttack = Attack + AttackLevel * Level;
		Hit = patterns.getHit(gameObject);
		Critical = patterns.getCritical(gameObject);

		Debug.Log ("att-" + RealAttack + "hit-" + Hit + "cri-" + Critical);
		var prefab = Resources.Load ("Skills/CrossSword");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		var attObj = effect.AddComponent<ObjectEffect> ();
		attObj.setAttribute (true , "y" , Speed * 2);
		var attRes = effect.AddComponent<AttackResolution> ();
		attRes.setSkillAttr ( TargetTag, TriggerPoint, Target, Effect, EffectTime, AttackType, SkillMethod,Speed, Attack, Hit, Critical);
		var effectRigid = effect.GetComponent<Rigidbody>();
		Vector3 forse;
		forse = gameObject.transform.forward;
		effect.transform.LookAt((TriggerPoint + forse));
		effectRigid.AddForce(forse * Speed * 100f);
		Destroy (effect, LifeTime);
	}

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkilType = "Attack2";
		SkillMethod = 2;
		Effect = "CrossSword_Effect";
		EffectTime = 2;
		AttackType = 1;		
		SpellRange = 20f;			
		Speed = 20f;
		LifeTime = 1f;			
		Attack = 100;
		AttackLevel = 45;
		Cooldown = 30f;
		TriggerPoint = new Vector3 ();
		Target = null;
	}

	public void Spell()
	{
		if (isTrigger) 
		{
			Debug.Log ("Skills in preparation。");
			return;
		}
		Target = patterns.getAttactTarget (gameObject);
		if (Target == null) 
		{
			Debug.Log ("no target");	
			return;
		} 
		else 
		{
			gameObject.transform.LookAt (Target.transform.position);
			patterns.getAllyAnimator(gameObject).SetTrigger ("Attack");
			isSpell = true;
		}
	}
}
