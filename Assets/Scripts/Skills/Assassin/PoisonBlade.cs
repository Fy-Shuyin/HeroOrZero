using UnityEngine;
using System.Collections;

public class PoisonBlade : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public bool isTrigger;
	public bool isSpell;
	public GameObject Target;

	//DataBase
	public int Level;
	public string SkillType;
	public int SkillMethod;
	private string Effect;
	private float EffectTime;
	private float LiveTime;				//存在时间
	private float Attack;				//攻击力
	private float Cooldown;				//冷却时间
	private float CooldownCount;
	private int attackCountdown;
	private bool isPoison;

	void Start () 
	{
		skillInitialize ();
	}

	void Update () 
	{
		if (isSpell) 
		{
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
		skillStatus ();
	}

	void skillStatus()
	{
		if (attackCountdown > 0) 
		{
			AnimatorStateInfo asi = patterns.getAllyAnimator (gameObject).GetCurrentAnimatorStateInfo (0);
			if (asi.IsName ("Attack") && isPoison) 
			{
				isPoison = false;
				attackCountdown--;
				StartCoroutine (Wait (0.5F));
			}
		}
	}

	IEnumerator Wait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		isPoison = true;
		Target = patterns.getAttactTarget (gameObject);
		Vector3 targetPoint = Target.transform.position;
		Collider collider = Target.GetComponent<Collider> ();
		targetPoint.x += (collider.bounds.size.x/2) * collider.transform.forward.normalized.x;
		targetPoint.y += (collider.bounds.size.y/2);
		targetPoint.z += (collider.bounds.size.z/2) * collider.transform.forward.normalized.z;
		var prefab = Resources.Load ("Skills/PoisonBlade");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = targetPoint;
		Destroy (effect, LiveTime);
		if (Target.GetComponent<PoisonBladeDebuff> () == null) 
		{
			var debuff = Target.AddComponent<PoisonBladeDebuff> ();
			debuff.setAttribute (Effect, Attack, LiveTime);
		}
		else
		{
			var debuff = Target.GetComponent<PoisonBladeDebuff> ();
			debuff.setAttribute (Effect, Attack, LiveTime);
		}
	}

	void skillInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkillType = "Debuff";
		SkillMethod = 5;
		Effect = "PoisonBlade_Effect";
		EffectTime = 10;
		Attack = 25;
		LiveTime = 1.5f;
		Cooldown = 15f;
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
		isPoison = true;
		attackCountdown = 5;
	}
}
