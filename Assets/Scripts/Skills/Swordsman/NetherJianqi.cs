using UnityEngine;
using System.Collections;

public class NetherJianqi : MonoBehaviour {

	FingerEvent finger = new FingerEvent ();

	public bool isskill;
	public bool isTrigger;
	public bool isSpell;
	private Vector3 TriggerPoint;
	private Vector3 TargetPoint;
	private string TargetTag;
	private bool TargetSelect;
	private int TargetLayer;

	//DataBase
	private int Level;
	private string SkilType;
	private string Effect;
	private float EffectTime;
	private bool IsFly;					//是否飛行
	private float SpellRange;			//飞行距离
	private float Speed;				//飛行速度
	private float LifeTime;				//存在时间
	private float Attack;				//攻击力
	private float AttackLevel;			//攻击力增长
	private float RealAttack;			//实际攻击力
	private float Hit;					//命中率
	private float Critical;				//暴击率
	private float Cooldown;				//冷却时间
	private float CooldownCount;

	void Start () 
	{
		if (gameObject.tag.Equals ("Ally")) 
		{
			TargetLayer = 1 << LayerMask.NameToLayer ("Enemy");
			TargetTag = "Enemy";
		} 
		else if (gameObject.tag.Equals ("Enemy")) 
		{
			TargetLayer = 1 << LayerMask.NameToLayer ("Ally");
			TargetTag = "Ally";
		}
		isskill = true;
		skllInitialize ();
	}
	
	void Update () 
	{
		if (isSpell) 
		{
			Collider[] cols = Physics.OverlapSphere(this.transform.position, SpellRange, TargetLayer);
			if (cols.Length > 0)
			{
				foreach (Collider player in cols)
				{
					float dis1 = 0;
					float dis2 = Vector3.Distance (transform.position, player.transform.position);
					if (dis2 > dis1) 
					{
						dis1 = dis2;
						TargetPoint = player.transform.position;
					}
				}
				skillStatus();
				isSpell = false;
			}
		}
		if (isTrigger)
		{	
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
		}
	}

	void skillStatus()
	{
		var attribute = gameObject.GetComponent<CharacterAttribute> ();
		TriggerPoint = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5f);
		LifeTime = 3f;
		RealAttack = Attack + AttackLevel * Level;
		Hit = attribute.Hit + attribute.HitAdditional;
		Critical = attribute.Critical + attribute.CriticalAdditional;
		var prefab = Resources.Load ("NetherJianqi");
		GameObject effect = Instantiate (prefab) as GameObject;
		effect.transform.position = TriggerPoint;
		var attRes = effect.AddComponent<AttackResolution> ();
		attRes.setSkillAttr (isskill , TargetTag, TriggerPoint, TargetPoint, Effect, EffectTime, IsFly, Speed, Attack, Hit, Critical);
		effect.transform.Translate ((TargetPoint - TriggerPoint) * Speed * Time.deltaTime);
		Destroy (effect, LifeTime);
	}

	void skllInitialize()
	{
		isTrigger = false;
		isSpell = false;
		Level = 1;
		SkilType = "Attack";
		Effect = "";
		EffectTime = 0;
		IsFly = true;		
		SpellRange = 30f;			
		Speed = 20f;
		LifeTime = 3f;			
		Attack = 75;
		AttackLevel = 30;
		Cooldown = 7f;
		TriggerPoint = new Vector3 ();
		TargetPoint = new Vector3 ();
	}
}
