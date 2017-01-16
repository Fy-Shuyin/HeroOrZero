using UnityEngine;
using System.Collections;

public class WarRant : MonoBehaviour {
	FingerEvent finger = new FingerEvent ();
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
	private float EffectTimeLevel;
	private int   AttackType;			//是否飛行
	private float SpellRange;			//飞行距离
	private float Speed;				//飛行速度
	private float LifeTime;				//存在时间
	private float Attack;				//攻击力
	private float AttackLevel;			//攻击力增长
	private float Hit;					//命中率
	private float Critical;				//暴击率
	private float Cooldown;				//冷却时间
	private float CooldownCount;

	void Start () 
	{
		if (gameObject.tag.Equals ("Ally")) 
		{
			TargetLayer = 1 << LayerMask.NameToLayer ("Ally");
			TargetTag = "Ally";
		} 
		else if (gameObject.tag.Equals ("Enemy")) 
		{
			TargetLayer = 1 << LayerMask.NameToLayer ("Enemy");
			TargetTag = "Enemy";
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
		TriggerPoint = gameObject.transform.position;
		float realAttack = patterns.getAttack(gameObject) * ((Attack + AttackLevel * Level)/100);
		float reakEffectTime = EffectTime + (EffectTimeLevel * Level);
		Debug.Log ("att-" + realAttack + "time-" + reakEffectTime);
		var prefab = Resources.Load ("Skills/WarRant");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		Destroy (effect, 2f);


		ArrayList TargetList = patterns.AlliesFriend (gameObject , SpellRange);
		if (TargetList.Count > 0) 
		{
			foreach (GameObject target in TargetList) 
			{
				var attUp = target.AddComponent<ATTUP> ();
				attUp.setSkillAttr (gameObject, realAttack, reakEffectTime, Effect);
			}	
		}

	}

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkilType = "Buff";
		SkillMethod = 4;
		Effect = "WarRant_Effect";
		EffectTime = 10;
		EffectTimeLevel = 0.5f;
		SpellRange = 20f;			
		Speed = 0f;
		LifeTime = 3f;			
		Attack = 25;//25%
		AttackLevel = 4;
		Cooldown = 20f;
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
		isSpell = true;
	}
}
