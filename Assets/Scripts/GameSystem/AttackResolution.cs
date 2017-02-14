using UnityEngine;
using System.Collections;

public class AttackResolution : MonoBehaviour {

	PatternsOfAttribute Attribute = new PatternsOfAttribute();

	private bool isSkill;
	private GameObject Trigger;
	private GameObject Target;
	private string TargetTag;
	private string Effect;				//特效
	private float EffectTime;			//特效持續時間
	private int AttackType;				//是否飛行
	private int SkillMethod;			//单体 群体
	private float Speed;				//飛行速度
	private float Angle;				//角度
	private float Attack;				//物理攻击力
	private float Hit;					//命中率
	private float Critical;				//暴击率
	private float TargetDefence;		//目标防御
	private float TargetDodge;			//目标闪避
	private bool Action;
	private Vector3 Vector;
	private float VerticalSpeed;
	private float HorizontalSpeed;
	private float TimeCount;

	void Update () 
	{
		if (Target == null) 
		{
			Action = false;
		}
		if (Action) 
		{
			if (Speed != 0) 
			{
/*				if (Angle != 0 && AttackType == 1) 
				{
					if (VerticalSpeed == 0 && HorizontalSpeed == 0) 
					{
						VerticalSpeed = Speed * Mathf.Sin (Angle * Mathf.PI / 180);
						HorizontalSpeed = Speed * Mathf.Cos (Angle * Mathf.PI / 180);
						Vector *= HorizontalSpeed * Time.deltaTime;
						TimeCount = 0;
					}
					TimeCount += Time.deltaTime;
					float time = VerticalSpeed / 2;
					Debug.Log ("垂直初始速度 " + VerticalSpeed + "减少量 " + (2 * TimeCount) + "速度 " + (VerticalSpeed - (2 * TimeCount)));
					if (VerticalSpeed - (2 * TimeCount) > 0) {

						Vector.y = (VerticalSpeed * TimeCount - TimeCount * TimeCount);
						Debug.Log ("垂直距离1 " + Vector.y );
					} else {
						Vector.y = -(TimeCount * TimeCount);
						Debug.Log ("垂直距离2 " + Vector.y );
					}

				}
*/
				if (AttackType == 1) 
				{
					Speed += 2;
				}
				Vector3 targetPoint = Target.transform.position;
				targetPoint.y += Target.GetComponent<Collider> ().bounds.size.y / 2;
				Vector = (targetPoint - gameObject.transform.position).normalized;
				Vector *= Speed * Time.deltaTime;
				gameObject.transform.position += Vector;
			}
		}
	}
	/// <summary>
	/// 設置普通攻擊數值
	/// </summary>
	/// <param name="triggerPoint">單位的位置</param>
	/// <param name="target">攻擊對象</param>
	/// <param name="attackType">攻擊類型</param>
	/// <param name="speed">攻擊速度(遠程攻擊有效)</param>
	/// <param name="angle">攻擊角度(遠程攻擊有效)</param>
	/// <param name="att">攻擊力</param>
	/// <param name="hit">命中率</param>
	/// <param name="cri">爆擊率</param>
	public void setAttAttr(GameObject trigger , GameObject target , int attackType , float speed , float angle , float att , float hit , float cri)
	{
		isSkill = false;
		Trigger = trigger;
		Target = target;
		TargetTag = target.tag;
		AttackType = attackType;
		Speed = speed * 2.5f;
		Angle = angle;
		Attack = att;
		Hit = hit;
		Critical = cri;
		Action = true;
		Destroy(gameObject,2f);
	}
	/// <summary>
	/// 設置技能攻擊數值
	/// </summary>
	/// <param name="triggerPoint">單位的位置</param>
	/// <param name="target">攻擊對象</param>
	/// <param name="eft">特效名稱</param>
	/// <param name="eftTime">特效持續時間</param>
	/// <param name="skillType">技能類型</param>
	/// <param name="speed">技能速度(遠程技能有效)</param>
	/// <param name="angle">技能飛行角度(遠程技能有效)</param>
	/// <param name="att">攻擊力</param>
	/// <param name="hit">命中率</param>
	/// <param name="cri">爆擊率</param>
	public void setSkillAttr( GameObject trigger , GameObject target , string eft , float eftTime , int skillMethod , float speed , float angle , float att , float hit , float cri , bool action)
	{
		isSkill = true;
		Trigger = trigger;
		Target = target;
		TargetTag = target.tag;
		Effect = eft;
		EffectTime = eftTime;
		SkillMethod = skillMethod;
		Speed = speed * 2.5f;
		Angle = angle;
		Attack = att;
		Hit = hit;
		Critical = cri;
		Action = action;
	}

	void OnTriggerEnter(Collider collider)
	{
		//碰觸到邊界的時候
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
		//當碰觸目標為守護對象時
		if (collider.tag == "Guardian" && Trigger.tag.Equals("Enemy"))
		{
			Destroy(gameObject);
			if (isSkill) 
			{
				if (Effect != null) 
				{
					loadEffect (collider , Effect , EffectTime);
				}
			}
			TargetDefence = collider.GetComponent<TownHallAttribute> ().Defence;
			int damage = (int)(Attack - TargetDefence);
			if (damage <= 0) 
			{
				damage = 1;
			}
			collider.GetComponent<TownHallAttribute> ().HealthPower -= damage;
		}
		//當碰觸到目標單位且不是守護對象時
		if (collider.tag == TargetTag && collider.tag != "Guardian")
		{
			///當是技能的時候判定特效
			if (isSkill) 
			{
				if (SkillMethod == 1) 
				{
					Destroy (gameObject);
				}
				if (Effect != "")
				{
					loadEffect (collider, Effect, EffectTime);
				}
			}
			else 
			{
				if (AttackType <= 1)
					Destroy (gameObject);
			}
			///防禦技能判定
			if (isSkillBarrier (collider)) 
			{
				return;
			}
				
			TargetDodge = Attribute.getDodge (collider.gameObject);
			if (Random.Range (0, 101) <= (int)(Hit - Hit * TargetDodge / 100)) 
			{
				if (Random.Range (0, 101) <= (int)Critical) 
				{
					Attack *= 2;
				}

				TargetDefence = Attribute.getDefence (collider.gameObject);
				int damage = (int)(TargetDefence - Attack);
				if (damage >= 0) 
				{
					damage = -1;
				}
				Attribute.setHealthPower (collider.gameObject, damage);
			}
		}
	}

	/// <summary>
	/// 當技能特效不為空的時候創造特效
	/// </summary>
	/// <param name="collider">單位的Collider</param>
	/// <param name="eftName">緊跟特效的名字</param>
	/// <param name="destroyTime">特效存在時間</param>
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
	/// <summary>
	/// Barrier技能防禦判定
	/// </summary>
	/// <returns><c>true</c>, 隨機數在概率以內, <c>false</c> 隨機數在概率外</returns>
	/// <param name="collider">單位的Collider</param>
	bool isSkillBarrier(Collider collider)
	{
		bool isExist = false;
		var barrier = collider.GetComponent<Barrier> ();
		if (barrier != null)
		{
			if (Random.Range (0, 101) < barrier.RealValue)
				isExist = true;
		}
		return isExist;
	}
}
