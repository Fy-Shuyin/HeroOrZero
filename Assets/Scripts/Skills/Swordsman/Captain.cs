using UnityEngine;
using System.Collections;

public class Captain : MonoBehaviour {

	PatternsOfAttribute patterns = new PatternsOfAttribute();

	public string TargetTag;
	private int TargetLayer;

	private int Level;
	public string SkilType;
	public int SkillMethod;
	private GameObject Effect;
	private string EffectName;
	private float Value;
	private float ValueLevel;
	public float RealValue;
	private float Range;
	private Vector3 TriggerPoint;

	void Start () 
	{
		skllInitialize ();
		if (gameObject.tag.Equals ("Ally")) 
		{
			TargetTag = "Ally";
		} 
		else if (gameObject.tag.Equals ("Enemy")) 
		{
			TargetTag = "Enemy";
		}
		var prefab = Resources.Load ("Skills/Captain");
		Effect = Instantiate (prefab) as GameObject;
		Effect.transform.SetParent(gameObject.transform);
		TriggerPoint = gameObject.transform.position;
		TriggerPoint.y = 0.1f;
		Effect.transform.position = TriggerPoint;
	}

	void Update () 
	{
		ArrayList targetList = patterns.AlliesFriend (gameObject, Range);
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

	void skllInitialize()
	{
		Level = 1;
		SkilType = "Buff";
		SkillMethod = 4;
		EffectName = "Captain_Effect";
		Value = 15;//15%
		ValueLevel = 3;
		RealValue = (Value + (ValueLevel * Level))/100;
		Range = 20;
		TriggerPoint = new Vector3 ();
	}
}
