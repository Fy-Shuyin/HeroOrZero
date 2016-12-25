using UnityEngine;
using System.Collections;

public class PatternsOfSkills
{
	PatternsOfAttribute attribute = new PatternsOfAttribute ();
	public Component getSkill (GameObject obj, string name)
	{
		var skill = obj.GetComponent (name);
		return skill;
	}

	public void setSkills(GameObject obj , ArrayList selectskills)
	{
		foreach (string skill in selectskills) 
		{
			if (skill == "NetherJianqi") 
			{
				obj.AddComponent<NetherJianqi> ();
			}
		}
	}

	public int getSkillTypeCount (GameObject obj, ArrayList name , string type)
	{
		int i = 0;
		foreach (string skill in name) 
		{
			if (type == getSkillType (obj, skill)) 
			{
				i++;
			}
		}
		return i;
	}

	public string getSkillType(GameObject obj,string name)
	{
		string type = "";

		if (name == "NetherJianqi") 
		{
			type = obj.GetComponent<NetherJianqi> ().SkilType;
		}

		return type;
	}

	public int getSkillMethod(GameObject obj,string name)
	{
		int method = 0;

		if (name == "NetherJianqi") 
		{
			method = obj.GetComponent<NetherJianqi> ().SkillMethod;
		}

		return method;
	}

	public void SkillSpell(GameObject obj , string name)
	{
		
	}
}
