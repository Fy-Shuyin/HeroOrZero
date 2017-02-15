using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	private int Level;
	public string SkillType;
	public int SkillMethod;
	private string Effect;
	private float Value;
	public int RealValue;
	private Vector3 TriggerPoint;

	void Start () 
	{
		skillInitialize ();
	}

	void skillInitialize()
	{
		Level = 1;
		SkillType = "Buff";
		SkillMethod = 4;
		Effect = "WarRant_Effect";		
		Value = 8;//8%
		RealValue = (int)Value;
		TriggerPoint = new Vector3 ();
	}
}
