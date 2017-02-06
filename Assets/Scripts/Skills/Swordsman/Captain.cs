using UnityEngine;
using System.Collections;

public class Captain : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	private int Level;
	public string SkillType;
	public int SkillMethod;
	private GameObject Effect;
	private string EffectName;
	private float Value;
	public float RealValue;
	private float Range;
	private Vector3 TriggerPoint;

	void Start () 
	{
		skllInitialize ();
		var prefab = Resources.Load ("Skills/Captain");
		Effect = Instantiate (prefab) as GameObject;
		Effect.transform.SetParent(gameObject.transform);
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y = 0.1f;
		Effect.transform.position = TriggerPoint;
	}

	void Update () 
	{
		bool isLive = patterns.TargetIsLive (gameObject);
		if (isLive) 
		{
			ArrayList targetList = patterns.AlliesFriendList (gameObject, Range);
			foreach(GameObject target in targetList)
			{
				var com = target.GetComponent<CaptainBuff> ();
				if (com == null) 
				{
					var buff = target.AddComponent<CaptainBuff> ();
					buff.setSkillAttr (gameObject, Range, RealValue, EffectName);
				}
			}	
		}
	}

	void skllInitialize()
	{
		Level = 1;
		SkillType = "Buff";
		SkillMethod = 4;
		EffectName = "Captain_Effect";
		Value = 15;//15%
		RealValue = Value/100;
		Range = 20;
		TriggerPoint = new Vector3 ();
	}
}
