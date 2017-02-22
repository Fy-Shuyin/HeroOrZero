using UnityEngine;
using System.Collections;

public class CrossSwords : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	public GameObject Target;

	//DataBase
	public int Level;
	public string SkillType;
	public int SkillMethod;
	private string Effect;
	private float EffectTime;
	private float SpellRange;			//飞行距离
	private float Speed;				//飛行速度
	private float Angle;				//飛行角度
	private float LifeTime;				//存在时间
	private float Attack;				//攻击力
	private float Hit;					//命中率
	private float Critical;				//暴击率
	public float Cooldown;				//冷却时间
	public float CooldownCount;

	void Start () 
	{
		skillInitialize ();
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
		TriggerPoint.x += gameObject.transform.forward.normalized.x * 3f;
		TriggerPoint.y += collider.bounds.size.y/2;
		TriggerPoint.z += gameObject.transform.forward.normalized.z * 3f;
		Hit = patterns.getHit(gameObject);
		Critical = patterns.getCritical(gameObject);

		var prefab = Resources.Load ("Skills/CrossSword");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		var attObj = effect.AddComponent<ObjectEffect> ();
		attObj.setAttribute (true , "y" , Speed * 2);
		var attRes = effect.AddComponent<AttackResolution> ();
		attRes.setSkillAttr (gameObject, Target, Effect, EffectTime, SkillMethod, Speed, Angle, Attack, Hit, Critical, false);
		var effectRigid = effect.GetComponent<Rigidbody>();
		Vector3 forse;
		forse = gameObject.transform.forward;
		effect.transform.LookAt((TriggerPoint + forse));
		effectRigid.AddForce(forse * Speed * 100f);
		Destroy (effect, LifeTime);
	}

	void skillInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Attack2";
		SkillMethod = 2;
		Effect = "CrossSword_Effect";
		EffectTime = 2;	
		SpellRange = 20f;			
		Speed = 20f;
		Angle = 0;
		LifeTime = 1f;			
		Attack = 145;
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
