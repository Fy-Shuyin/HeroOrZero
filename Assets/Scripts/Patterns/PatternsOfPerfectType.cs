using UnityEngine;
using System.Collections;

public class PatternsOfPerfectType : PatternsOfThinking
{
	PatternsOfAttribute attribute = new PatternsOfAttribute();
	PatternsOfSkills skills = new PatternsOfSkills();
	public void PerfectType(GameObject obj)
	{
		if (attribute.getHealthPower (obj) < attribute.getHealthPowerMax (obj) * 0.7) {
			Debug.Log ("HP");
			if (skills.getSkillTypeCount (obj, attribute.getActiveSkillSelect (obj), "Defence") > 0) 
			{
				Debug.Log ("DefenceSkill");	
			} else {
				int command = Instruction (obj);
				if (attribute.getCommand (obj) != command) {
					Debug.Log ("Command");
					if (command == 1) {
						attribute.setCommand (obj, 1);
					} else if (command == 0) {
						attribute.setCommand (obj, 0);
					} else if (command == -1) {
						attribute.setCommand (obj, -1);
					}
				} else {
					int friendHp = FriendHPAverage (obj);
					if (friendHp < 5) {
					
					} else {
						int numEnemy = attribute.NumberOfEnemies (obj, attribute.getSightRange (obj));
						if (numEnemy > 0 && numEnemy < 2) {
						
						} else if (numEnemy > 2) {
						}
					}
				}
			} 
		}
	}
}
