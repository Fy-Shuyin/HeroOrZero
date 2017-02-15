using UnityEngine;
using System.Collections;

public class FireShield : MonoBehaviour {

	PatternsOfAttribute attribute = new PatternsOfAttribute();

	private int Level;
	public string SkillType;
	public int SkillMethod;
	public GameObject Effect;
	private string EffectName;
	private float Value;
	private Vector3 TriggerPoint;

	void Start () 
	{
		skillInitialize ();
		Effect = loadEffect ();
	}

	void Update () 
	{
		if (attribute.TargetIsLive (gameObject)) 
		{
			if (Effect == null) 
			{
				Effect = loadEffect ();
			}
			Effect.transform.Rotate (new Vector3(0,0,1));
		}
		else 
		{
			if (Effect != null) 
			{
				attribute.setAttack (gameObject, -Value);
				attribute.setDefence (gameObject, -Value);
				Destroy (Effect);
			}
		}
	}

	void skillInitialize()
	{
		Level = 1;
		SkillType = "Buff";
		SkillMethod = 4;
		Effect = null;
		EffectName = "Skills/FireShield";
		Value = 10;
		TriggerPoint = Vector3.zero;
	}

	GameObject loadEffect()
	{
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y += gameObject.GetComponent<Collider> ().bounds.size.y * 1.2f; 
		var prefab = Resources.Load (EffectName);
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		effect.transform.SetParent (gameObject.transform);

		attribute.setAttack (gameObject, Value);
		attribute.setDefence (gameObject, Value);
		return effect;
	}
}
