using UnityEngine;
using System.Collections;

public class PatternsOfThinking
{
	PatternsOfAttribute attribute = new PatternsOfAttribute();

	public int Instruction(GameObject obj)
	{
		int numFriend = attribute.getFriendNum (obj);
		int numEnemy = attribute.NumberOfEnemies (obj, attribute.getSightRange (obj));
		int instruction = 0;
		if (numFriend > numEnemy) 
		{
			instruction = 1;
		}
		if (numFriend == numEnemy) 
		{
			instruction = 0;
		}
		if (numFriend < numEnemy) 
		{
			instruction = -1;
		}
		return instruction;
	}

	public int FriendHPAverage(GameObject obj)
	{
		int average = 0;
		ArrayList friends = attribute.AlliesFriend (obj);
		ArrayList HP = new ArrayList ();
		if (friends.Count > 0) 
		{
			foreach (GameObject friend in friends) 
			{
				HP.Add(attribute.getHealthPower (friend));
			}
			foreach (int i in HP) 
			{
				average += i;
			}
			average /= HP.Count;
			average /= attribute.getHealthPowerMax ((GameObject)friends[0]);
		}
		return average;
	}
}
