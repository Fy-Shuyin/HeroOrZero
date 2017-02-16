using UnityEngine;
using System.Collections;

public class PatternsOfThinking
{
	PatternsOfAttribute attribute = new PatternsOfAttribute();
	PatternsOfSkills skillsAttribute = new PatternsOfSkills ();

	/// <summary>
	/// 获取命令
	/// </summary>
	/// <param name="obj">英雄</param>
	/// <returns> 友军大于敌人 1 友军等于敌人 0 友军小于敌人 -1
	public int Instruction(GameObject obj)
	{
		int numFriend = attribute.AlliesFriendList(obj,0).Count;
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

	/// <summary>
	/// 友军的平均生命值
	/// </summary>
	/// <returns>生命值的平均值</returns>
	/// <param name="obj">英雄但闻</param>
	public int FriendHPAverage(GameObject obj)
	{
		int average = 0;
		ArrayList friends = attribute.AlliesFriendList (obj , 0);
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

	/// <summary>
	/// 获取强敌
	/// </summary>
	/// <returns>强敌的GameObject</returns>
	/// <param name="obj">单位</param>
	/// <param name="range">范围</param>
	public GameObject getStrongTarget(GameObject obj, float range)
	{
		GameObject strongTarget = null;
		ArrayList targetList = attribute.TargetList (attribute.getAttactTarget (obj), range);
		foreach (GameObject target in targetList) 
		{
			int damage = (int)attribute.Damage (obj, target);
			if (damage < attribute.getHealthPowerMax (target) * 5 / 100) 
			{
				strongTarget =  target;
				break;
			}
		}
		return strongTarget;
	}
	/// <summary>
	/// 获取生命值最少的对象
	/// </summary>
	/// <returns>对象的GameObject</returns>
	/// <param name="obj">单位</param>
	/// <param name="range">范围</param>
	public GameObject getLeastHealthTarget(GameObject obj, float range)
	{
		GameObject leastHealthEnemy = null;
		Collider[] targetList = attribute.BloodVolumeList(obj, range);
		if (targetList.Length > 0)
			leastHealthEnemy = targetList [0].gameObject;
		return leastHealthEnemy;
	}
	/// <summary>
	/// 获取生命值最多的对象
	/// </summary>
	/// <returns>对象的GameObject</returns>
	/// <param name="obj">单位</param>
	/// <param name="range">范围</param>
	public GameObject getHighestHealthTarget(GameObject obj, float range)
	{
		GameObject highestHealthEnemy = null;
		Collider[] targetList = attribute.BloodVolumeList(obj, range);
		if (targetList.Length > 0)
			highestHealthEnemy = targetList [targetList.Length-1].gameObject;
		return highestHealthEnemy;
	}
	/// <summary>
	/// 逃跑
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="percent">小于最大生命值的百分比 与 逃跑几率</param>
	public bool isRunAway(GameObject obj, int percent)
	{
		if (attribute.getHealthPower (obj) < attribute.getHealthPowerMax (obj) * percent / 100) 
		{
			if (percent > Random.Range (0, 101)) 
			{
				Vector3 point = obj.transform.position;
				point -= obj.transform.forward.normalized * 15;
				Debug.Log (point);
				attribute.setMoveStage (obj, point);
				return true;
			}
		}
		return false;
	}
}
