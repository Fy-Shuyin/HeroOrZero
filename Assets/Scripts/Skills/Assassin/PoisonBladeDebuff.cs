using UnityEngine;
using System.Collections;

public class PoisonBladeDebuff : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();
	private string EffectName;
	private int Attack;
	private float LiveTime;
	private float cooldown;
	private bool loadEffect;

	void Update () 
	{
		if (loadEffect) 
		{
			Vector3 targetPoint = gameObject.transform.position;
			Collider collider = gameObject.GetComponent<Collider> ();
			targetPoint.y += (collider.bounds.size.y/2);
			var prefab = Resources.Load ("SkillEffect/" + EffectName);
			GameObject effect = Instantiate (prefab) as GameObject;
			effect.transform.position = targetPoint;
			effect.transform.SetParent(gameObject.transform);
			effect.transform.localScale = new Vector3 (1,1,1);
			Destroy (effect, LiveTime);
			loadEffect = false;
		}
		LiveTime -= Time.deltaTime;
		if (LiveTime <= 0) 
		{
			Destroy (gameObject.GetComponent<PoisonBladeDebuff> ());
		}
		cooldown -= Time.deltaTime;
		if (cooldown < 0)
			cooldown = 0;
		if (cooldown == 0) 
		{
			patterns.setHealthPower (gameObject, Attack);
			cooldown = 1;
		}
	}

	public void setAttribute(string effectName, float attack, float liveTime)
	{
		EffectName = effectName;
		Attack = (int)attack * -1;
		LiveTime = liveTime;
		loadEffect = true;
	}
}
