using UnityEngine;
using System.Collections;

public class CaptainBuff : MonoBehaviour {

	PatternsOfAttribute Attribute = new PatternsOfAttribute();

	private GameObject Trigger;
	private float ValueUp;
	private float attValue;
	private float defValue;
	private GameObject effect;
	private float Range;
	private bool runEffect;

	void Update () 
	{
		if (runEffect) 
		{
			bool targetIsLive = Attribute.TargetIsLive (gameObject);
			bool triggerIsLive = Attribute.TargetIsLive (Trigger);
			if (Trigger == null || !triggerIsLive || !targetIsLive) 
			{
				lessSkillAtt (gameObject , ValueUp);
				runEffect = false;
				return;
			} 
			Vector3 triggerPoint = Trigger.transform.position;
			Vector3 objPoint = gameObject.transform.position;
			triggerPoint.y = 0;
			objPoint.y = 0;
			float distance = Vector3.Distance (triggerPoint,objPoint);
			effect.transform.position = effectPoint(gameObject);
			if (distance > Range) 
			{
				lessSkillAtt (gameObject , ValueUp);
				runEffect = false;
			} 
		}
	}

	public Vector3 effectPoint (GameObject obj)
	{
		Vector3 point = new Vector3 ();

		point = obj.transform.position;
		point.y = 0.1f;

		return point;
	}

	public void setSkillAttr(GameObject trigger, float range, float value, string effectName)
	{
		this.Trigger = trigger;
		this.ValueUp = value;
		this.Range = range+1;

		var prefab = Resources.Load ("SkillEffect/" + effectName);
		effect = Instantiate (prefab) as GameObject;
		effect.transform.position = effectPoint(gameObject);
		effect.transform.SetParent(gameObject.transform);
		effect.transform.localScale = new Vector3 (1,1,1);
		runEffect = true;
		addSkillAtt (gameObject, ValueUp);
	}

	public void addSkillAtt(GameObject target, float value)
	{
		attValue = Attribute.getAttack (target) * value;
		defValue = Attribute.getDefence (target) * value;
		Attribute.setAttack (target, attValue);
		Attribute.setDefence (target, defValue);
	}

	public void lessSkillAtt(GameObject target, float value)
	{
		Attribute.setAttack (target, -attValue);
		Attribute.setDefence (target, -defValue);
		Destroy (effect);
		Destroy (target.GetComponent<CaptainBuff> ());
	}
}
