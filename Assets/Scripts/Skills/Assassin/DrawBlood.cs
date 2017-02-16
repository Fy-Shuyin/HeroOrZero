using UnityEngine;
using System.Collections;

public class DrawBlood : MonoBehaviour {

	PatternsOfAttribute Patterns = new PatternsOfAttribute();

	private int Level;
	public string SkillType;
	public  int SkillMethod;
	public GameObject Target;
	public GameObject CurrentTarget;
	private int Value;	//回復する値

	void Start () 
	{
		SkillInitialize ();
	}

	void Update () 
	{
		if (Target == null) {
			Target = Patterns.getAttactTarget (gameObject);
		}
		else 
		{
			if (!Patterns.TargetIsLive (Target) && Target != CurrentTarget) 
			{
				Debug.Log ("DrawBlood");
				var prefab = Resources.Load ("Skills/DrawBlood");
				GameObject effect = Instantiate (prefab) as GameObject;
				Vector3 point = gameObject.transform.position;
				point.y = gameObject.GetComponent<Collider> ().bounds.size.y/2;
				effect.transform.position = point;
				Destroy (effect, 1.5f);
				if (Patterns.getHealthPower (gameObject) + Value > Patterns.getHealthPowerMax (gameObject))
				{
					Patterns.resetHealthPower (gameObject);
					CurrentTarget = Target;
				} 
				else 
				{
					Patterns.setHealthPower (gameObject, Value);
					CurrentTarget = Target;
				}
			}
		}
	}

	void SkillInitialize()
	{
		Level = 1;
		SkillType = "Permanent";
		SkillMethod = 7;			
		Value = 50;
		Target = null;
		CurrentTarget = null;
	}
}
