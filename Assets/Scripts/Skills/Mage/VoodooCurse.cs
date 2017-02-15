using UnityEngine;
using System.Collections;

public class VoodooCurse : MonoBehaviour {
	
	PatternsOfAttribute attribute = new PatternsOfAttribute();

	private int Level;
	public string SkillType;
	public int SkillMethod;
	private GameObject Effect;
	private string EffectName;
	private float Attack;
	private Vector3 TriggerPoint;

	void Start () {
		skillInitialize ();
	}

	void skillInitialize()
	{
		Level = 1;
		SkillType = "Buff";
		SkillMethod = 4;
		Effect = null;
		EffectName = "Skills/VoodooCurse";
		Attack = 3;//3%
		TriggerPoint = Vector3.zero;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "AttackResolution")
		{
			GameObject trigger = collider.GetComponent<AttackResolution> ().Target; 
			GameObject target = collider.GetComponent<AttackResolution> ().Trigger;
			if (trigger.name == gameObject.name) 
			{
				TriggerPoint = target.transform.position;
				TriggerPoint.y += target.GetComponent<Collider> ().bounds.size.y / 2;
				var prefab = Resources.Load (EffectName);
				GameObject effect = Instantiate (prefab) as GameObject;
				effect.transform.position = TriggerPoint;
				effect.transform.SetParent (trigger.transform);
				Destroy (effect, 1f);
				int realAttack = (int)(attribute.getHealthPower (target) * Attack / -100);
				attribute.setHealthPower (target, realAttack);
			}
		}
	}
}
