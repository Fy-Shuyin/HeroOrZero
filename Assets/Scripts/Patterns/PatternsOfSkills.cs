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
	/// <summary>
	/// 设定技能
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="selectskills">技能表</param>
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
	/// <summary>
	/// 释放技能
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="name">技能名称</param>
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
		if (name == "BloodthirstyGlare") 
		{
			obj.GetComponent<BloodthirstyGlare> ().Spell ();
			return;
		}
	}
	/// <summary>
	/// 获取技能方法号码
	/// </summary>
	/// <returns>技能方法号码</returns>
	/// <param name="obj">单位</param>
	/// <param name="skillName">技能名字</param>
	public int getSkillMethod(GameObject obj,string skillName)
	{
		int method = 0;

		if (skillName == "NetherJianqi") 
		{
			method = obj.GetComponent<NetherJianqi> ().SkillMethod;
		}
		if (skillName == "WarRant") 
		{
			method = obj.GetComponent<WarRant> ().SkillMethod;
		}
		if (skillName == "CrossSwords") 
		{
			method = obj.GetComponent<CrossSwords> ().SkillMethod;
		}
		if (skillName == "Barrier") 
		{
			method = obj.GetComponent<Barrier> ().SkillMethod;
		}
		if (skillName == "Captain") 
		{
			method = obj.GetComponent<Captain> ().SkillMethod;
		}
		if (skillName == "InstantChop") 
		{
			method = obj.GetComponent<InstantChop> ().SkillMethod;
		}
		if (skillName == "PoisonBlade") 
		{
			method = obj.GetComponent<PoisonBlade> ().SkillMethod;
		}
		if (skillName == "ShadowAttack") 
		{
			method = obj.GetComponent<ShadowAttack> ().SkillMethod;
		}
		if (skillName == "LoneWalker") 
		{
			method = obj.GetComponent<LoneWalker> ().SkillMethod;
		}
		if (skillName == "DrawBlood") 
		{
			method = obj.GetComponent<DrawBlood> ().SkillMethod;
		}
		if (skillName == "SpiritSpear") 
		{
			method = obj.GetComponent<SpiritSpear> ().SkillMethod;
		}
		if (skillName == "NightAndDream") 
		{
			method = obj.GetComponent<NightAndDream> ().SkillMethod;
		}
		if (skillName == "BloodthirstyGlare") 
		{
			method = obj.GetComponent<BloodthirstyGlare> ().SkillMethod;
		}
		if (skillName == "FireShield") 
		{
			method = obj.GetComponent<FireShield> ().SkillMethod;
		}
		if (skillName == "VoodooCurse") 
		{
			method = obj.GetComponent<VoodooCurse> ().SkillMethod;
		}
		return method;
	}

	public bool isSpell(GameObject obj,string skillName)
	{
		bool isSpell = false;

		if (skillName == "NetherJianqi") 
		{
			isSpell = obj.GetComponent<NetherJianqi> ().isTrigger;
		}
		if (skillName == "WarRant") 
		{
			isSpell = obj.GetComponent<WarRant> ().isTrigger;
		}
		if (skillName == "CrossSwords") 
		{
			isSpell = obj.GetComponent<CrossSwords> ().isTrigger;
		}
		if (skillName == "InstantChop") 
		{
			isSpell = obj.GetComponent<InstantChop> ().isTrigger;
		}
		if (skillName == "PoisonBlade") 
		{
			isSpell = obj.GetComponent<PoisonBlade> ().isTrigger;
		}
		if (skillName == "ShadowAttack") 
		{
			isSpell = obj.GetComponent<ShadowAttack> ().isTrigger;
		}
		if (skillName == "SpiritSpear") 
		{
			isSpell = obj.GetComponent<SpiritSpear> ().isTrigger;
		}
		if (skillName == "NightAndDream") 
		{
			isSpell = obj.GetComponent<NightAndDream> ().isTrigger;
		}
		if (skillName == "BloodthirstyGlare") 
		{
			isSpell = obj.GetComponent<BloodthirstyGlare> ().isTrigger;
		}
		return !isSpell;
	}
	public float getSkillCooldown(GameObject obj,string skillName)
	{
		float cooldown = 0;

		if (skillName == "NetherJianqi") 
		{
			cooldown = obj.GetComponent<NetherJianqi> ().CooldownCount / obj.GetComponent<NetherJianqi> ().Cooldown;
		}
		if (skillName == "WarRant") 
		{
			cooldown = obj.GetComponent<WarRant> ().CooldownCount / obj.GetComponent<WarRant> ().Cooldown;
		}
		if (skillName == "CrossSwords") 
		{
			cooldown = obj.GetComponent<CrossSwords> ().CooldownCount / obj.GetComponent<CrossSwords> ().Cooldown;
		}
		if (skillName == "InstantChop") 
		{
			cooldown = obj.GetComponent<InstantChop> ().CooldownCount / obj.GetComponent<InstantChop> ().Cooldown;
		}
		if (skillName == "PoisonBlade") 
		{
			cooldown = obj.GetComponent<PoisonBlade> ().CooldownCount / obj.GetComponent<PoisonBlade> ().Cooldown;
		}
		if (skillName == "ShadowAttack") 
		{
			cooldown = obj.GetComponent<ShadowAttack> ().CooldownCount / obj.GetComponent<ShadowAttack> ().Cooldown;
		}
		if (skillName == "SpiritSpear") 
		{
			cooldown = obj.GetComponent<SpiritSpear> ().CooldownCount / obj.GetComponent<SpiritSpear> ().Cooldown;
		}
		if (skillName == "NightAndDream") 
		{
			cooldown = obj.GetComponent<NightAndDream> ().CooldownCount / obj.GetComponent<NightAndDream> ().Cooldown;
		}
		if (skillName == "BloodthirstyGlare") 
		{
			cooldown = obj.GetComponent<BloodthirstyGlare> ().CooldownCount / obj.GetComponent<BloodthirstyGlare> ().Cooldown;
		}
		return cooldown;
	}
	/// <summary>
	/// 获取指定类型的技能
	/// </summary>
	/// <returns>技能的List</returns>
	/// <param name="obj">单位</param>
	/// <param name="skillMethod">技能数字的号码</param>
	public ArrayList skillList(GameObject obj, int skillMethod)
	{
		ArrayList skills = new ArrayList ();
		foreach (string skill in attribute.getActiveSkillSelect(obj)) 
		{
			if (skillMethod == getSkillMethod (obj, skill)) 
			{
				skills.Add (skill);
			}
		}
		return skills;
	}
	/// <summary>
	/// 释放技能是否成功
	/// </summary>
	/// <returns><c>true</c> 成功 <c>false</c> 失败 </returns>
	/// <param name="obj">单位</param>
	/// <param name="target">目标</param>
	/// <param name="skillMethod">技能种类</param>
	public bool isSpellSuccess(GameObject obj, GameObject target, bool isChange, int skillMethod)
	{
		ArrayList skillsList = new ArrayList ();
		skillsList = skillList (obj, skillMethod);
		if (skillsList.Count > 0) 
		{
			foreach (string skill in skillsList) 
			{
				if (isSpell (obj, skill)) 
				{
					attribute.setSpellStage (obj, target, isChange, skill);
					return true;
				}
			}
		}
		return false;
	}
}
