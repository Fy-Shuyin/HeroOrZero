using UnityEngine;
using System.Collections;

public class InstantChop : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	private Vector3 TriggerSize;
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
	private float LiveTime;				//存在时间
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

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Attack1";
		SkillMethod = 1;
		Effect = "";
		EffectTime = 0;	
		SpellRange = 15f;			
		Speed = 50f;
		Angle = 0;
		LiveTime = 1.5f;			
		Attack = 90;
		Cooldown = 9f;
		TriggerPoint = new Vector3 ();
		Target = null;
	}

	void skillStatus()
	{
		Collider collider = gameObject.GetComponent<Collider> ();
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y += collider.bounds.size.y/2;
		Hit = patterns.getHit(gameObject);
		Critical = patterns.getCritical(gameObject);

		Debug.Log ("att-" + Attack + "hit-" + Hit + "cri-" + Critical);
		var prefab = Resources.Load ("Skills/InstantChop");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		Destroy(effect,LiveTime);
		StartCoroutine(Wait(1.0F));
		collider.enabled = false;
		TriggerSize = gameObject.transform.localScale; 
		gameObject.transform.localScale = new Vector3 (0,0,0);
	}

	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		gameObject.transform.localScale = TriggerSize;
		Collider collider = gameObject.GetComponent<Collider> ();
		collider.enabled = true;
		Vector3 targetPoint = Target.transform.position;
		targetPoint.x -= Target.transform.forward.normalized.x * 5;
		targetPoint.z -= Target.transform.forward.normalized.z * 5;
		gameObject.transform.position = targetPoint;
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y += collider.bounds.size.y/2;
		patterns.getAllyAnimator(gameObject).SetTrigger ("Attack");
		var prefab = Resources.Load ("AttackEffect/AttackResolution");
		GameObject AttackResolution = Instantiate (prefab) as GameObject;
		AttackResolution.transform.position = TriggerPoint;
		var attRes = AttackResolution.GetComponent<AttackResolution> ();
		attRes.setSkillAttr (TriggerPoint, Target, Effect, EffectTime, SkillMethod, Speed, Angle, Attack, Hit, Critical, true);
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
			gameObject.GetComponent<HeroController> ().ChangeToStopStage (1f);
			gameObject.transform.LookAt (Target.transform.position);
			isSpell = true;
		}
	}
}
