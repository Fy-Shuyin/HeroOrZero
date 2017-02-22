using UnityEngine;
using System.Collections;

public class ShadowAttack : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	public GameObject Target;
	public GameObject AttackTarget;
	private Vector3 TargetPoint;

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
	public float Cooldown;				//冷却时间
	public float CooldownCount;
	private float AttackCoolDown;
	private int AttackNum;

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
		if (AttackNum > 0 && AttackCoolDown == 0) 
		{
			Collider collider = AttackTarget.GetComponent<Collider> ();
			TargetPoint = AttackTarget.transform.position;
			TargetPoint.x -= AttackTarget.transform.forward.normalized.x * (collider.bounds.size.x / 2);
			TargetPoint.y = 0;
			TargetPoint.z -= AttackTarget.transform.forward.normalized.z * (collider.bounds.size.z / 2);
			gameObject.transform.position = TargetPoint;
			gameObject.transform.LookAt (AttackTarget.transform);
			TriggerPoint = gameObject.transform.position;
			TriggerPoint.y += collider.bounds.size.y / 2;
			patterns.getAllyAnimator (gameObject).SetTrigger ("Attack");
			var prefab2 = Resources.Load ("AttackEffect/AttackResolution");
			GameObject AttackResolution = Instantiate (prefab2) as GameObject;
			AttackResolution.transform.position = TriggerPoint;
			var attRes = AttackResolution.GetComponent<AttackResolution> ();
			attRes.setSkillAttr (gameObject, AttackTarget, Effect, EffectTime, SkillMethod, Speed, Angle, Attack, Hit, Critical, true);
			ArrayList targetList = new ArrayList ();
			if (Target != null) 
			{
				targetList = patterns.TargetList (Target, 20);
			}
			else
			{
				targetList = patterns.TargetList (gameObject, 20);
			}

			if (targetList.Count > 0) 
			{
				int random = Random.Range (0, targetList.Count);
				AttackTarget = (GameObject)targetList [random];
				AttackNum--;
				AttackCoolDown = 0.5f;
			} 
			else 
			{
				AttackNum = 0;
			}
		}
		else
		{
			AttackCoolDown -= Time.deltaTime;
			if (AttackCoolDown < 0) 
			{
				AttackCoolDown = 0;
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
		SpellRange = 10f;			
		Speed = 50f;
		Angle = 0;
		LiveTime = 4f;			
		Attack = 65;
		Cooldown = 30f;
		Target = null;
		TargetPoint = new Vector3 ();
		TriggerPoint = new Vector3 ();
		AttackNum = 0;
	}

	void skillStatus()
	{
		Target = patterns.getAttactTarget (gameObject);
		TargetPoint = Target.transform.position;
		TargetPoint.y = 11;
		var prefab1 = Resources.Load ("Skills/ShadowAttack");
		GameObject effect = Instantiate (prefab1) as GameObject;
		effect.transform.position = TargetPoint;
		Destroy(effect,LiveTime);
		AttackTarget = Target;
		Hit = patterns.getHit(gameObject);
		Critical = patterns.getCritical(gameObject);
		gameObject.GetComponent<HeroController> ().ChangeToStopStage (4f);
		Collider collider = gameObject.GetComponent<Collider> ();
		collider.enabled = false;
		isSpell = false;
		StartCoroutine(Wait(LiveTime));
	}

	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Collider collider = gameObject.GetComponent<Collider> ();
		collider.enabled = true;
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
			isSpell = true;
			AttackNum = 7;
			AttackCoolDown = 0;
		}
	}
}
