using UnityEngine;
using System.Collections;

public class PatternsOfThinker : PatternsOfThinking
{
	PatternsOfAttribute patternsAttribute = new PatternsOfAttribute ();
	PatternsOfSkills skillsAttribute = new PatternsOfSkills();

	public bool runThinker(GameObject obj)
	{
		//1.单体技能
		if(patternsAttribute.getAttactTarget(obj) != null)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 1))
			{
				return true;
			}
		}
		//2.群体技能
		if(patternsAttribute.NumberOfEnemies(obj,patternsAttribute.getSightRange(obj)) > 1)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 2)) 
			{
				return true;
			}
		}
		//3.防御技能
		if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 3)) 
		{
			return true;
		}
		//4.减益技能
		if(patternsAttribute.getAttactTarget(obj) != null)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 5))
			{
				return true;
			}
		}
		//5.对友军的命令
		if (patternsAttribute.getCommand (obj) != Instruction (obj)) 
		{
			patternsAttribute.setCommand (obj, Instruction (obj));
			return true;
		}

		//6.增益技能
		if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 4))
		{
			return true;
		}

		//7.回复技能
		if (FriendHPAverage(obj) < 150) 
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 6))
			{
				return true;
			}
		}
		if (isRunAway (obj, 10)) 
		{
			return true;
		}

		return false;
	}

	private GameObject getTarget(GameObject obj)
	{
		GameObject target = getStrongTarget(obj,patternsAttribute.getSightRange(obj));
		if (target == null) 
		{
			target = getHighestHealthTarget (obj, patternsAttribute.getSightRange (obj));
		}
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
