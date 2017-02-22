using UnityEngine;
using System.Collections;

public class SpiritSpear : MonoBehaviour 
{
	PatternsOfAttribute Attribute = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	public GameObject Target;
	private bool TargetSelect;
	private int TargetLayer;

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

	void skillInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Attack1";
		SkillMethod = 1;
		Effect = "";
		EffectTime = 0;	
		SpellRange = 20f;			
		Speed = 25f;
		Angle = 0;
		LifeTime = 3f;			
		Attack = 100;
		Cooldown = 7f;
		TriggerPoint = new Vector3 ();
		Target = null;
	}

	void skillStatus()
	{
		Collider collider = gameObject.GetComponent<Collider> ();
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.x += (collider.bounds.size.x/2) * collider.transform.forward.normalized.x;
		TriggerPoint.y += collider.bounds.size.y;
		TriggerPoint.z += (collider.bounds.size.z/2) * collider.transform.forward.normalized.z;
		Hit = Attribute.getHit(gameObject);
		Critical = Attribute.getCritical(gameObject);

		var prefab = Resources.Load ("Skills/SpiritSpear");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		effect.transform.LookAt (Target.transform.position);
		var attRes = effect.AddComponent<AttackResolution> ();
		attRes.setSkillAttr (gameObject, Target, Effect, EffectTime, SkillMethod, Speed, Angle, Attack, Hit, Critical, true);
		Destroy(effect,1.5f);
	}

	public void Spell()
	{
		if (isTrigger) 
		{
			Debug.Log ("Skills in preparation。");
			return;
		}
		Target = Attribute.getAttactTarget (gameObject);
		if (Target == null) 
		{
			Debug.Log ("no target");	
			return;
		} else 
		{
			gameObject.transform.LookAt (Target.transform.position);
			Attribute.getAllyAnimator(gameObject).SetTrigger ("Attack");
			isSpell = true;
		}
	}
}
