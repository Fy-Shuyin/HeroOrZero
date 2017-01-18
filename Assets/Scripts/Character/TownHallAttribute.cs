using UnityEngine;
using System.Collections;

public class TownHallAttribute : MonoBehaviour {

	public bool isLife;
	public int HealthPower;				//生命值
	public float Defence;				//防御力

	void Start () 
	{
		AttributeInitialize ();
	}


	void Update () 
	{
		if (HealthPower <= 0)
			isLife = false;
	}

	void AttributeInitialize()
	{
		isLife = true;
		HealthPower = 2000;
		Defence = 0;
	}
}
