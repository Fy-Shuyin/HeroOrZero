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
			if (skill == "WarRant") 
			{
				obj.AddComponent<WarRant> ();
			}
			if (skill == "CrossSwords") 
			{
				obj.AddComponent<CrossSwords> ();
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
		if (name == "WarRant") 
		{
			type = obj.GetComponent<WarRant> ().SkilType;
		}
		if (name == "CrossSwords") 
		{
			type = obj.GetComponent<CrossSwords> ().SkilType;
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
		if (name == "WarRant") 
		{
			method = obj.GetComponent<WarRant> ().SkillMethod;
		}
		if (name == "CrossSwords") 
		{
			method = obj.GetComponent<CrossSwords> ().SkillMethod;
		}

		return method;
	}

	public void SkillSpell(GameObject obj , string name)
	{
		if (name == "NetherJianqi") 
		{
			obj.GetComponent<NetherJianqi> ().Spell ();
			return;
		}
		if (name == "WarRant") 
		{
			obj.GetComponent<WarRant> ().Spell ();
			return;
		}
		if (name == "CrossSwords") 
		{
			obj.GetComponent<CrossSwords> ().Spell ();
			return;
		}
	}
}
