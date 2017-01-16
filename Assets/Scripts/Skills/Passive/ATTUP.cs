using UnityEngine;
using System.Collections;

public class ATTUP : MonoBehaviour {

	PatternsOfAttribute Attribute = new PatternsOfAttribute();

	private GameObject target;
	private float attUp;
	public float time;
	private GameObject effect;
	private string effectName;
	private bool runEffect;


	void Update () 
	{
		if (runEffect) 
		{
			effect.transform.position = effectPoint(target);

			if (time == 0) 
			{
				Attribute.setAttack (gameObject, -attUp);
				Destroy (effect);
				Destroy (target.GetComponent<ATTUP> ());
			} 
			else 
			{
				time -= Time.deltaTime;
				if (time < 0) 
				{
					time = 0;
				}
			}	
		}
	}

	public Vector3 effectPoint (GameObject obj)
	{
		Vector3 point = new Vector3 ();

		point = obj.transform.position;
		Collider collider = obj.GetComponent<Collider> ();
		point.y += collider.bounds.size.y;

		return point;
	}

	public void setSkillAttr(GameObject target, float attUp, float time , string effectName)
	{
		this.target = target;
		this.attUp = attUp;
		this.time = time;
		this.effectName = effectName;

		var prefab = Resources.Load ("SkillEffect/" + effectName);
		effect = Instantiate (prefab) as GameObject;
		effect.transform.position = effectPoint(target);
		Attribute.setAttack (target, attUp);

		runEffect = true;
	}
}
