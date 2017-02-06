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
			if (skill == "Barrier") 
			{
				obj.AddComponent<Barrier> ();
			}
			if (skill == "Captain") 
			{
				obj.AddComponent<Captain> ();
			}
			if (skill == "InstantChop") 
			{
				obj.AddComponent<InstantChop> ();
			}
			if (skill == "PoisonBlade") 
			{
				obj.AddComponent<PoisonBlade> ();
			}
			if (skill == "ShadowAttack") 
			{
				obj.AddComponent<ShadowAttack> ();
			}
			if (skill == "LoneWalker") 
			{
				obj.AddComponent<LoneWalker> ();
			}
			if (skill == "DrawBlood") 
			{
				obj.AddComponent<DrawBlood> ();
			}
			if (skill == "SpiritSpear") 
			{
				obj.AddComponent<SpiritSpear> ();
			}
			if (skill == "NightAndDream") 
			{
				obj.AddComponent<NightAndDream> ();
			}
			if (skill == "BloodthirstyGlare") 
			{
				obj.AddComponent<BloodthirstyGlare> ();
			}
			if (skill == "FireShield") 
			{
				obj.AddComponent<FireShield> ();
			}
			if (skill == "VoodooCurse") 
			{
				obj.AddComponent<VoodooCurse> ();
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
			type = obj.GetComponent<NetherJianqi> ().SkillType;
		}
		if (name == "WarRant") 
		{
			type = obj.GetComponent<WarRant> ().SkillType;
		}
		if (name == "CrossSwords") 
		{
			type = obj.GetComponent<CrossSwords> ().SkillType;
		}
		if (name == "Barrier") 
		{
			type = obj.GetComponent<Barrier> ().SkillType;
		}
		if (name == "Captain") 
		{
			type = obj.GetComponent<Captain> ().SkillType;
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
		if (name == "Barrier") 
		{
			method = obj.GetComponent<Barrier> ().SkillMethod;
		}
		if (name == "Captain") 
		{
			method = obj.GetComponent<Captain> ().SkillMethod;
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
		if (name == "InstantChop") 
		{
			obj.GetComponent<InstantChop> ().Spell ();
			return;
		}
		if (name == "PoisonBlade") 
		{
			obj.GetComponent<PoisonBlade> ().Spell ();
			return;
		}
		if (name == "ShadowAttack") 
		{
			obj.GetComponent<ShadowAttack> ().Spell ();
			return;
		}
		if (name == "SpiritSpear") 
		{
			obj.GetComponent<SpiritSpear> ().Spell ();
			return;
		}
		if (name == "NightAndDream") 
		{
			obj.GetComponent<NightAndDream> ().Spell ();
			return;
		}
	}
}
