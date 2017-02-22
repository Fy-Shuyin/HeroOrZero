using UnityEngine;
using System.Collections;

public class WarRant : MonoBehaviour 
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
	private float EffectTimeLevel;
	private float SpellRange;			//飞行距离
	private float LiveTime;				//存在时间
	private float Attack;				//攻击力
	private float Cooldown;				//冷却时间
	private float CooldownCount;

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
		TriggerPoint = gameObject.transform.position;
		float realAttack = patterns.getAttack(gameObject) * (Attack/100);
		float reakEffectTime = EffectTime + (EffectTimeLevel * Level);
		var prefab = Resources.Load ("Skills/WarRant");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		Destroy (effect, 2f);

		var attUp = gameObject.AddComponent<ATTUP> ();
		attUp.setSkillAttr (gameObject, realAttack, reakEffectTime, Effect);
		ArrayList TargetList = patterns.AlliesFriendList (gameObject , SpellRange);
		if (TargetList.Count > 0) 
		{
			foreach (GameObject target in TargetList) 
			{
				attUp = target.AddComponent<ATTUP> ();
				attUp.setSkillAttr (target, realAttack, reakEffectTime, Effect);
			}	
		}

	}

	void skillInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Buff";
		SkillMethod = 4;
		Effect = "WarRant_Effect";
		EffectTime = 10;
		EffectTimeLevel = 0.5f;
		SpellRange = 30f;
		LiveTime = 3f;			
		Attack = 25;//25%
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
