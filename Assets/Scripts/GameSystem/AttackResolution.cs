using UnityEngine;
using System.Collections;

public class AttackResolution : MonoBehaviour {

	public bool isSkill;
	public string Effect;
	public float EffectTime;
	public Vector3 TriggerPoint;
	public Vector3 TargetPoint;
	public string TargetTag;
	public bool IsFly;					//是否飛行
	public float Speed;					//飛行速度
	public float Attack;				//物理攻击力
	public float Hit;					//命中率
	public float Critical;				//暴击率
	public float TargetDefence;			//目标防御
	public float TargetDodge;			//目标闪避


	void Start () 
	{
		Destroy(gameObject,1f);
	}
	
	void Update () 
	{
		if (!isSkill) 
		{
			
		}
		else 
		{
			if (IsFly) 
			{

			} else 
			{
			}
		}
	}
	public void setAttAttr(bool isSkill , string targetTag , float att , float hit , float cri)
	{
		this.isSkill = isSkill;
		this.TargetTag = targetTag;
		this.Attack = att;
		this.Hit = hit;
		this.Critical = cri;
	}

	public void setSkillAttr(bool isSkill , string targetTag , Vector3 triggerPoint , Vector3 targetPoint , string eft , float eftTime , bool isFly , float speed , float att , float hit , float cri)
	{
		this.isSkill = isSkill;
		this.TargetTag = targetTag;
		this.TriggerPoint = triggerPoint;
		this.TargetPoint = targetPoint;
		this.Effect = eft;
		this.EffectTime = eftTime;
		this.IsFly = isFly;
		this.Speed = speed;
		this.Attack = att;
		this.Hit = hit;
		this.Critical = cri;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Field")
		{
			Destroy(gameObject);
			loadEffect (collider.transform.position , Effect , EffectTime);
			//图标 文字生成

		}

		if (collider.tag == "Field")
		{
			Destroy(gameObject);
			loadEffect (collider.transform.position , Effect , EffectTime);
		}

		if (collider.tag == TargetTag)
		{
			Destroy(gameObject);
			if (isSkill) 
			{
				if (Effect != null) 
				{
					loadEffect (collider.transform.position , Effect , EffectTime);
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
				if (TargetTag == "Ally") 
				{
					if (damage <= 0) 
					{
						damage = 1;
					}
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

	void loadEffect(Vector3 Point , string eftName , float destroyTime)
	{
		if (!eftName.Equals ("")) 
		{
			var prefab = Resources.Load(eftName);
			GameObject effect = Instantiate(prefab) as GameObject;
			effect.transform.position = Point;
			Destroy(effect , destroyTime);

		}
	}
}
