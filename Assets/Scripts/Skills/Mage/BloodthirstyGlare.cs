using UnityEngine;
using System.Collections;

public class BloodthirstyGlare : MonoBehaviour {

	PatternsOfAttribute Attribute = new PatternsOfAttribute();

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
	private float SpellRange;			//影响距离
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
		SkillType = "Attack2";
		SkillMethod = 1;
		Effect = "Skills/BloodthirstyGlare";
		EffectTime = 0;	
		SpellRange = 10f;
		LifeTime = 3f;			
		Attack = 80;
		Cooldown = 30f;
		TriggerPoint = new Vector3 ();
		Target = null;
	}

	void skillStatus()
	{
		gameObject.GetComponent<HeroController> ().ChangeToStopStage (2f);
		TriggerPoint = Target.transform.position;
		TriggerPoint.y = 0;
		Hit = Attribute.getHit(gameObject);
		Critical = Attribute.getCritical(gameObject);

		var prefab = Resources.Load (Effect);
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		var effectMethod = effect.AddComponent<BloodthirstyGlareEffect> ();
		effectMethod.setAttribute (Attribute.HostileTag (gameObject), SpellRange, Attack, Hit, Critical);
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
		}
		Attribute.getAllyAnimator(gameObject).SetTrigger ("Attack");
		isSpell = true;
	}
}
