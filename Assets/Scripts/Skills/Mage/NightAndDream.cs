using UnityEngine;
using System.Collections;

public class NightAndDream : MonoBehaviour 
{
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
	private float SpellRange;			//影响范围
	private float LiveTime;				//存在时间
	private int  Value;				//回复力
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
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y += gameObject.GetComponent<Collider> ().bounds.size.y;
		var prefab = Resources.Load ("Skills/" + Effect);
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		effect.transform.SetParent (gameObject.transform);
		if (Attribute.getHealthPower (gameObject) + Value > Attribute.getHealthPowerMax (gameObject))
			Attribute.resetHealthPower (gameObject);
		else
			Attribute.setHealthPower (gameObject, Value);
		Destroy (effect, EffectTime);
		StartCoroutine (Wait (1F));
	}

	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		ArrayList targetList = Attribute.AlliesFriendList (gameObject, SpellRange);
		if (targetList.Count > 0) 
		{
			foreach (GameObject target in targetList) 
			{
				Vector3 point = target.transform.position;
				point.y += target.GetComponent<Collider> ().bounds.size.y; 
				var prefab = Resources.Load ("Skills/" + Effect);
				GameObject effect = Instantiate (prefab) as GameObject;
				effect.transform.position = point;
				effect.transform.SetParent (target.transform);
				if (Attribute.getHealthPower (target) + Value > Attribute.getHealthPowerMax (target))
					Attribute.resetHealthPower (target);
				else
					Attribute.setHealthPower (target, Value);
				Destroy (effect, EffectTime);
			}
		}
	}

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Reply";
		SkillMethod = 6;
		Effect = "NightAndDream";
		EffectTime = 3;
		SpellRange = 15f;
		LiveTime = 3f;			
		Value = 100;
		Cooldown = 2f;
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
