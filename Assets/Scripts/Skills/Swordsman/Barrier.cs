using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	private int Level;
	public string SkilType;
	public int SkillMethod;
	private string Effect;
	private float Value;
	private float ValueLevel;
	public int RealValue;
	private Vector3 TriggerPoint;

	void Start () 
	{
		skllInitialize ();
	}

	void Update () 
	{
	
	}

	void skllInitialize()
	{
		Level = 1;
		SkilType = "Buff";
		SkillMethod = 4;
		Effect = "WarRant_Effect";		
		Value = 5;//25%
		ValueLevel = 3;
		RealValue = (int)(Value + (ValueLevel * Level));
		TriggerPoint = new Vector3 ();
	}
}
