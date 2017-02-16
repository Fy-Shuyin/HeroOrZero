using UnityEngine;
using System.Collections;

public class PatternsOfCharacter
{
	PatternsOfAttribute attribute = new PatternsOfAttribute ();

	/// <summary>
	/// 1.Reformer
	/// 2.Helper
	/// 3.Motivator
	/// 4.Romantic
	/// 5.Thinker
	/// 6.Skeptic
	/// 7.Enthusiast
	/// 8.Leader
	/// 9.Peacemaker
	/// </summary>
	public string CharacterType(GameObject obj , int value)
	{
		string type = "";
		if (value == 1) 
		{
			ReformerAttribute (obj);
			type = "Reformer";
		}
		if (value == 2) 
		{
			ReformerAttribute (obj);
			type = "Helper";
		}
		if (value == 3) 
		{
			ReformerAttribute (obj);
			type = "Motivator";
		}
		if (value == 4) 
		{
			ReformerAttribute (obj);
			type = "Romantic";
		}
		if (value == 5) 
		{
			ThinkerAttribute (obj);
			type = "Thinker";
		}
		if (value == 6) 
		{
			ReformerAttribute (obj);
			type = "Skeptic";
		}
		if (value == 7) 
		{
			ReformerAttribute (obj);
			type = "Enthusiast";
		}
		if (value == 8) 
		{
			ReformerAttribute (obj);
			type = "Leader";
		}
		if (value == 9) 
		{
			ReformerAttribute (obj);
			type = "Peacemaker";
		}
		return type;
	}

	private void ReformerAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , 2);
		attribute.setHealthPowerMax (obj, Random.Range (10, 50));
		attribute.setAttack (obj, Random.Range (-5, 5));
		attribute.setDefence (obj, Random.Range (5, 10));
		attribute.setDexterity (obj, Random.Range (-5, -2));
		attribute.setAgility (obj, Random.Range (-10, -3));
	}
	private void HelperAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , 0);
		attribute.setHealthPowerMax (obj, Random.Range (-5, 5));
		attribute.setAttack (obj, Random.Range (-5, 5));
		attribute.setDefence (obj, Random.Range (5, 10));
		attribute.setDexterity (obj, Random.Range (-5, -2));
		attribute.setAgility (obj, Random.Range (3, 8));
	}
	private void MotivatorAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , -1);
		attribute.setHealthPowerMax (obj, Random.Range (20, 40));
		attribute.setAttack (obj, Random.Range (0, 5));
		attribute.setDefence (obj, Random.Range (-10, 0));
		attribute.setDexterity (obj, Random.Range (1, 5));
		attribute.setAgility (obj, Random.Range (1, 5));
	}
	private void RomanticAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , 1);
		attribute.setHealthPowerMax (obj, Random.Range (-10, 10));
		attribute.setAttack (obj, Random.Range (3, 7));
		attribute.setDefence (obj, Random.Range (3, 7));
		attribute.setDexterity (obj, Random.Range (-2, 2));
		attribute.setAgility (obj, Random.Range (-2, 2));
	}
	private void ThinkerAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , -2);
		attribute.setHealthPowerMax (obj, Random.Range (10, 50));
		attribute.setAttack (obj, Random.Range (5, 15));
		attribute.setDefence (obj, Random.Range (5, 15));
		attribute.setDexterity (obj, Random.Range (-10, -5));
		attribute.setAgility (obj, Random.Range (-10, -5));
	}
	private void SkepticAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , 0);
		attribute.setHealthPowerMax (obj, Random.Range (10, 30));
		attribute.setAttack (obj, Random.Range (1, 5));
		attribute.setDefence (obj, Random.Range (1, 5));
		attribute.setDexterity (obj, Random.Range (-2, 3));
		attribute.setAgility (obj, Random.Range (-2, 3));
	}
	private void EnthusiastAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , -3);
		attribute.setHealthPowerMax (obj, Random.Range (20, 40));
		attribute.setAttack (obj, Random.Range (5, 10));
		attribute.setDefence (obj, Random.Range (5, 10));
		attribute.setDexterity (obj, Random.Range (10, 15));
		attribute.setAgility (obj, Random.Range (10, 15));
	}
	private void LeaderAttribute(GameObject obj)
	{
		attribute.setLeaderShip (obj , 3);
		attribute.setHealthPowerMax (obj, Random.Range (-30, 10));
		attribute.setAttack (obj, Random.Range (5, 15));
		attribute.setDefence (obj, Random.Range (-15, 5));
		attribute.setDexterity (obj, Random.Range (-5, 5));
		attribute.setAgility (obj, Random.Range (-5, 5));
	}
	private void PeacemakerAttribute(GameObject obj)
	{
		//補正なし
	}

	public bool runThinkType(GameObject obj, string typeName)
	{
		if (typeName == "Reformer") 
		{
			return new PatternsOfReformer ().runReformer (obj);
		}
		if (typeName == "Helper") 
		{
			return new PatternsOfHelper ().runHelper (obj);
		}
		if (typeName == "Motivator") 
		{
			return new PatternsOfMotivator ().runMotivator (obj);
		}
		if (typeName == "Romantic") 
		{
			return new PatternsOfRomantic ().runRomantic (obj);
		}
		if (typeName == "Thinker") 
		{
			return new PatternsOfThinker ().runThinker (obj);
		}
		if (typeName == "Skeptic") 
		{
			return new PatternsOfSkeptic ().runSkeptic (obj);
		}
		if (typeName == "Enthusiast") 
		{
			return new PatternsOfEnthusiast ().runEnthusiast (obj);
		}
		if (typeName == "Leader") 
		{
			return new PatternsOfLeader ().runLeader (obj);
		}
		if (typeName == "Romantic") 
		{
			return new PatternsOfPeacemaker ().runPeacemaker (obj);
		}
		return false;
	}
}
