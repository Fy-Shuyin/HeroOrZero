﻿using UnityEngine;
using System.Collections;

public class PatternsOfRomantic :  PatternsOfThinking
{
	PatternsOfAttribute patternsAttribute = new PatternsOfAttribute ();
	PatternsOfSkills skillsAttribute = new PatternsOfSkills();

	public bool runRomantic(GameObject obj)
	{
		//1.逃跑
		isRunAway (obj, 15);
		//2.对友军的命令
		if (patternsAttribute.getCommand (obj) != Instruction (obj)) 
		{
			patternsAttribute.setCommand (obj, Instruction (obj));
			return true;
		}
		//3.增益技能
		if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 4))
		{
			return true;
		}
		//4.回复技能
		if (FriendHPAverage(obj) < 150) 
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 6))
			{
				return true;
			}
		}
		//5.减益技能
		if(patternsAttribute.getAttactTarget(obj) != null)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 5))
			{
				return true;
			}
		}
		//6.群体技能
		if(patternsAttribute.NumberOfEnemies(obj,patternsAttribute.getSightRange(obj)) > 1)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), isChange(getTarget(obj)), 2)) 
			{
				return true;
			}
		}
		//7.单体技能
		if(patternsAttribute.getAttactTarget(obj) != null)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), isChange(getTarget(obj)), 1))
			{
				return true;
			}
		}
		return false;
	}

	private GameObject getTarget(GameObject obj)
	{
		GameObject target = getStrongTarget(obj,patternsAttribute.getSightRange(obj));
		target = getHighestHealthTarget (obj, patternsAttribute.getSightRange (obj));
		return target;
	}

	private bool isChange(GameObject target)
	{
		if (target != null)
			return true;
		else
			return false;
	}
}