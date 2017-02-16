using UnityEngine;
using System.Collections;

public class PatternsOfEnthusiast :PatternsOfThinking
{
	PatternsOfAttribute patternsAttribute = new PatternsOfAttribute ();
	PatternsOfSkills skillsAttribute = new PatternsOfSkills();

	public bool runEnthusiast(GameObject obj)
	{
		//1.减益技能
		if(patternsAttribute.getAttactTarget(obj) != null)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 5))
			{
				return true;
			}
		}
		//2.单体技能
		if(patternsAttribute.getAttactTarget(obj) != null)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), isChange(getTarget(obj)), 1))
			{
				return true;
			}
		}
		//3.群体技能
		if(patternsAttribute.NumberOfEnemies(obj,patternsAttribute.getSightRange(obj)) > 1)
		{
			if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), isChange(getTarget(obj)), 2)) 
			{
				return true;
			}
		}
		//4.防御技能
		if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 3)) 
		{
			return true;
		}
		//5.增益技能
		if (skillsAttribute.isSpellSuccess (obj, getTarget(obj), false, 4))
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
