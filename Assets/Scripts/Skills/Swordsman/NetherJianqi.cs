using UnityEngine;
using System.Collections;

public class NetherJianqi : MonoBehaviour 
{

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
	private float Cooldown;				//冷却时间
	private float CooldownCount;

	void Start () 
	{
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
		TriggerPoint.x += (collider.bounds.size.x/2) * collider.transform.forward.normalized.x;
		TriggerPoint.y += (collider.bounds.size.y/2);
		TriggerPoint.z += (collider.bounds.size.z/2) * collider.transform.forward.normalized.z;
		Hit = patterns.getHit(gameObject);
		Critical = patterns.getCritical(gameObject);

		Debug.Log ("att-" + Attack + "hit-" + Hit + "cri-" + Critical);
		var prefab = Resources.Load ("Skills/NetherJianqi");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		effect.transform.LookAt (Target.transform.position);
		var attRes = effect.AddComponent<AttackResolution> ();
		attRes.setSkillAttr (TriggerPoint, Target, Effect, EffectTime, SkillMethod, Speed, Angle, Attack, Hit, Critical, true);
		Destroy(effect,1.5f);
	}

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Attack1";
		SkillMethod = 1;
		Effect = "NetherJianqi_Effect";
		EffectTime = 1;	
		SpellRange = 20f;			
		Speed = 20f;
		Angle = 0;
		LifeTime = 3f;			
		Attack = 75;
		Cooldown = 7f;
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
		} else 
		{
			gameObject.transform.LookAt (Target.transform.position);
			patterns.getAllyAnimator(gameObject).SetTrigger ("Attack");
			isSpell = true;
		}
	}
}
