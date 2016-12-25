using UnityEngine;
using System.Collections;

public class PatternsOfCharacter : PatternsOfPerfectType
{
	PatternsOfAttribute attribute = new PatternsOfAttribute ();
	public string CharcterType(GameObject obj , int value)
	{
		string type = "";
		if (value == 1) 
		{
			PerfectAttribute (obj);
			type = "Perfect Type";
		}

		return type;
	}
	/// <summary>
	/// 1.Perfect type
	/// </summary>
	public void runCharcterType(GameObject obj)
	{
		string type = attribute.getCharcterType (obj).ToLower().Replace(" ","");
		if (type == "perfecttype") 
		{
			PerfectType (obj);
		}
	}
	public void PerfectAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , 2);
		attribute.setHealthPowerMax (obj, Random.Range (10, 50));
		attribute.setAttack (obj, Random.Range (-5, 5));
		attribute.setDefence (obj, Random.Range (5, 10));
		attribute.setDexterity (obj, Random.Range (-5, -2));
		attribute.setAgility (obj, Random.Range (-10, -3));
	}
}
