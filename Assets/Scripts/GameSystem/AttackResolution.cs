using UnityEngine;
using System.Collections;

public class AttackResolution : MonoBehaviour {

	public bool isSkill;
	public string Effect;
	public float EffectTime;
	public Vector3 TriggerPoint;
	public GameObject Target;
	public string TargetTag;
	public int AttackType;				//是否飛行
	public int SkillType;				//单体 群体
	public float Speed;					//飛行速度
	public float Attack;				//物理攻击力
	public float Hit;					//命中率
	public float Critical;				//暴击率
	public float TargetDefence;			//目标防御
	public float TargetDodge;			//目标闪避
	public bool Action;

	void Update () 
	{
		if (Target == null || TriggerPoint == null) 
		{
			Action = false;
		}
		if (Action) 
		{
			Vector3 vector = (Target.transform.position - TriggerPoint).normalized;
			if (isSkill) 
			{
				if (AttackType == 1 && SkillType == 1)
				{
					vector *= Speed * 2 * Time.deltaTime;
					gameObject.transform.position += vector;
				}
			} 
			else
			{
				vector *= 3 * Time.deltaTime;
				gameObject.transform.position += vector;
			}
		}
	}
	public void setAttAttr(string targetTag ,  Vector3 triggerPoint , GameObject target , int attackType , float speed , float att , float hit , float cri)
	{
		this.isSkill = false;
		this.AttackType = attackType;
		this.Speed = speed;
		this.TargetTag = targetTag;
		this.TriggerPoint = triggerPoint;
		this.Target = target;
		this.Attack = att;
		this.Hit = hit;
		this.Critical = cri;
		this.Action = true;
		Destroy(gameObject,1.5f);
	}

	public void setSkillAttr(string targetTag , Vector3 triggerPoint , GameObject target , string eft , float eftTime , int attackType , int skillType , float speed , float att , float hit , float cri)
	{
		this.isSkill = true;
		this.TargetTag = targetTag;
		this.TriggerPoint = triggerPoint;
		this.Target = target;
		this.Effect = eft;
		this.EffectTime = eftTime;
		this.AttackType = attackType;
		this.SkillType = skillType;
		this.Speed = speed;
		this.Attack = att;
		this.Hit = hit;
		this.Critical = cri;
		this.Action = true;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Finish")
		{
			Destroy(gameObject);
			if (isSkill) 
			{
				if (Effect != null) 
				{
					loadEffect (collider , Effect , EffectTime);
				}
			}
			//图标 文字生成

		}

		if (collider.tag == "Field")
		{
			Destroy(gameObject);
			if (isSkill) 
			{
				if (Effect != null) 
				{
					loadEffect (collider , Effect , EffectTime);
				}
			}
			//图标 文字生成

		}

		if (collider.tag == "Guardian")
		{
			Destroy(gameObject);
			if (isSkill) 
			{
				if (Effect != null) 
				{
					loadEffect (collider , Effect , EffectTime);
				}
			}
		}

		if (collider.tag == TargetTag && collider.tag != "Guardian")
		{
			if (SkillType <= 1) 
			{
				Destroy(gameObject);
			}
			if (isSkill) 
			{
				if (Effect != null) 
				{
					loadEffect (collider , Effect , EffectTime);
				}
			}
			if (TargetTag == "Ally") 
			{
				Component com = collider.GetComponent<HeroController> ();
				if (com != null)
				{
					TargetDefence = collider.GetComponent<HeroController> ().Defence + collider.GetComponent<HeroController> ().DefenceAdditional;
					TargetDodge = collider.GetComponent<HeroController> ().Dodge + collider.GetComponent<HeroController> ().DodgeAdditional;
				}
				else
				{
					TargetDefence = collider.GetComponent<FriendController> ().Defence + collider.GetComponent<FriendController> ().DefenceAdditional;
					TargetDodge = collider.GetComponent<FriendController> ().Dodge + collider.GetComponent<FriendController> ().DodgeAdditional;
				}
			}
			if (TargetTag == "Enemy") 
			{
				TargetDefence = collider.GetComponent<EnemyController> ().Defence + collider.GetComponent<EnemyController> ().DefenceAdditional;
				TargetDodge = collider.GetComponent<EnemyController> ().Dodge + collider.GetComponent<EnemyController> ().DodgeAdditional;
			}

			if (Random.Range (0, 101) <= (int)(Hit - Hit * TargetDodge/100)) 
			{
				if (Random.Range (0, 101) <= (int)Critical) 
				{
					Attack *= 2;
				}

				int damage = (int)(Attack - TargetDefence);
				if (damage <= 0) 
				{
					damage = 1;
				}
				if (TargetTag == "Ally") 
				{
					Component com = collider.GetComponent<HeroController> ();
					if (com != null) 
					{
						collider.GetComponent<HeroController> ().HealthPower -= damage;
					}
					else
					{
						collider.GetComponent<FriendController> ().HealthPower -= damage;
					}
				}
				if (TargetTag == "Enemy") 
				{
					collider.GetComponent<EnemyController> ().HealthPower -= damage;
				}
			}
		}
	}

	void loadEffect(Collider collider , string eftName , float destroyTime)
	{
		if (!eftName.Equals ("")) 
		{
			var prefab = Resources.Load("SkillEffect/"+eftName);
			GameObject effect = Instantiate(prefab) as GameObject;
			Vector3 point = collider.transform.position;
			point.x += (collider.bounds.size.x/2) * collider.transform.forward.normalized.x;
			point.y += (collider.bounds.size.y/2);
			point.z += (collider.bounds.size.z/2) * collider.transform.forward.normalized.z;
			effect.transform.position = point;
			effect.transform.rotation = collider.transform.rotation;
			Destroy(effect , destroyTime);

		}
	}
}
