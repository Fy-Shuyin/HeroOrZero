using UnityEngine;
using System.Collections;

public class BloodthirstyGlareEffect : MonoBehaviour {

	PatternsOfAttribute attribute;

	private string subEffectName;
	private float time;
	private Vector3 sizeChangeValue;

	private string targetTag;
	private float spellRange;
	private float attack;
	private float hit;
	private float cri;

	void Start () 
	{
		attribute = new PatternsOfAttribute ();
		subEffectName = "SkillEffect/BloodthirstyGlare_Effect";
		time = 0;
		sizeChangeValue = new Vector3 (1f, 1f, 1f);

		gameObject.transform.localScale = Vector3.zero;
	}

	void Update () 
	{
		time += Time.deltaTime;
		if (time <= 1) 
		{
			gameObject.transform.localScale += sizeChangeValue * Time.deltaTime;
		}
		if(time >1 && time <= 2) 
		{
			gameObject.transform.localScale -= sizeChangeValue * Time.deltaTime;
		}
		if (time > 2) 
		{
			var prefab = Resources.Load (subEffectName);
			GameObject subEffect = Instantiate (prefab) as GameObject;
			subEffect.transform.position = gameObject.transform.position;
			Destroy (subEffect,2f);
			Destroy (gameObject);
		}
	}

	public void setAttribute(string targetTag, float spellRange, float att, float hit, float cri)
	{
		this.targetTag = targetTag;
		this.spellRange = spellRange;
		this.attack = att;
		this.hit = hit;
		this.cri = cri;
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log ("targetTag - " + targetTag + "  colliderTag - " + collider.tag);
		if (collider.tag == targetTag) {
			Debug.Log ("IN");
			Damage (collider.gameObject);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == targetTag) {
			Debug.Log ("OUT");
			Damage (collider.gameObject);
		}
	}

	void Damage(GameObject obj)
	{
		float def = attribute.getDefence (obj);
		float dod = attribute.getDodge (obj);
		if (Random.Range (0, 101) <= (int)(hit - hit * dod / 100)) 
		{
			if (Random.Range (0, 101) <= (int)cri) 
			{
				attack *= 2;
			}
			int damage = (int)(def - attack);
			if (damage >= 0) 
			{
				damage = -1;
			}
			attribute.setHealthPower (obj, damage);
			Debug.Log ("Damage");
		}
	}
}
