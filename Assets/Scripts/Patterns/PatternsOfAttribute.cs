using UnityEngine;
using System.Collections;

public class PatternsOfAttribute
{
	///<summary>
	/// 目标是否存活
	/// </summary>
	/// <returns> 存活返回true
	public bool TargetIsLive(GameObject target)
	{
		bool isLife = true;
		if (getHealthPower (target) <= 0) 
		{
			isLife = false;
		}
		return isLife;
	}
	///<summary>
	/// 设置目标存活状态
	/// </summary>
	public void setTargetLive(GameObject target)
	{
		if (target.transform.tag == "Ally") 
		{
			if (target.GetComponent<HeroController> () != null)
				target.GetComponent<HeroController> ().IsLive = true;
			else
				target.GetComponent<FriendController> ().IsLive = true;
		}
		if (target.transform.tag == "Enemy") 
		{
			target.GetComponent<EnemyController> ().IsLive = true;
		}
	}
	///<summary>
	/// 友军的数量 包含自己
	/// </summary>
	/// <returns>友军的数量
	public int NumberOfAllies(GameObject obj , float range)
	{
		string Tag = obj.gameObject.tag;
		int Layer = 1 << LayerMask.NameToLayer (Tag);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
		return cols.Length;
	}
	///<summary>
	/// 友军的集合
	/// </summary>
	/// <param name="range">范围 当为0时包含自己 不为0时范围内除自己意外友军</param>
	/// <returns>友军的List
	public ArrayList AlliesFriendList(GameObject obj , float range)
	{
		ArrayList alliesList = new ArrayList ();
		int Layer = 1 << LayerMask.NameToLayer ("Ally");
		if (range == 0) 
		{
			Collider[] cols = Physics.OverlapSphere(obj.transform.position, 100 , Layer);
			foreach (Collider col in cols) 
			{

				if (col.gameObject.GetComponent<FriendController> () != null) 
				{
					if (col.gameObject.GetComponent<FriendController> ().Leader.Equals (obj)) 
					{
						alliesList.Add (col.gameObject);
					}
				}	
			}
		}
		else
		{
			Collider[] cols = Physics.OverlapSphere(obj.transform.position, range , Layer);
			foreach (Collider col in cols) 
			{
				if(col.gameObject != obj)
					alliesList.Add (col.gameObject);
			}
		}
		return alliesList;
	}
	///<summary>
	/// 全场景敌人数量
	/// </summary>
	/// <returns> 数量
	public int NumberOfEnemies()
	{
		int Layer = 1 << LayerMask.NameToLayer ("Enemy");;
		Collider[] cols = Physics.OverlapSphere(new Vector3() , 500f , Layer);
		return cols.Length;
	}
	///<summary>
	/// 范围内敌人数量
	/// </summary>
	/// <param name="range">范围</param>
	/// <returns> 数量
	public int NumberOfEnemies(GameObject obj , float range)
	{
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
		return cols.Length;
	}
	/// <summary>
	/// 目标单位的周围相同类型的集合 包含自己
	/// </summary>
	/// <returns>集合</returns>
	/// <param name="obj">单位</param>
	/// <param name="range">范围</param>
	public ArrayList TargetList(GameObject obj , float range)
	{
		ArrayList enemyList = new ArrayList ();
		int Layer = 1 << obj.layer;
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range , Layer);
		foreach (Collider col in cols) 
		{
			enemyList.Add (col.gameObject);
		}
		return enemyList;
	}
	///<summary>
	/// 判断对对方的伤害是否大于当前生命值的10%
	/// </summary>
	/// <returns> 若大于 返回1 若小于 返回0
	public int Damage(GameObject tigger , GameObject target)
	{
		int MinMax = -1;
		float att = getAttack(tigger);
		float def = getDefence(target);
		float hp = getHealthPower(target);
		if (att - def > hp * 0.1) 
		{
			MinMax = 1;
		}
		if (att - def <= hp * 0.1) 
		{
			MinMax = 0;
		}
		return MinMax;
	}
	///<summary>
	/// 判断对自己的伤害是否大于当前生命值的10%
	/// </summary>
	/// <returns> 若大于 返回1 若小于 返回0
	public int Injury(GameObject tigger , GameObject target)
	{
		int MinMax = -1;
		float hp = getHealthPower(tigger);
		float def = getDefence(tigger);
		float att = getAttack(target);
		if (att - def > hp * 0.1) 
		{
			MinMax = 1;
		}
		if (att - def <= hp * 0.1) 
		{
			MinMax = 0;
		}
		return MinMax;
	}
	///<summary>
	/// 范围内Object的HPList
	/// </summary>
	/// <param name="range">范围</param>
	/// <returns>HP从少到多排血的ColliderLIst
	public Collider[] BloodVolumeList(GameObject obj , float range)
	{
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
		if (cols.Length > 0) {
			int i, j = 1;
			Collider select = cols [0];
			while (j < cols.Length) {
				for (i = 0; i < cols.Length - j; i++) {
					float a = getHealthPower (cols [i].gameObject);
					float b = getHealthPower (cols [i + 1].gameObject);
					if (a > b) {
						select = cols [i];
						cols [i] = cols [i + 1];
						cols [i + 1] = select;
						break;
					}
				}
				j++;
			}
		}
		return cols;
	}
	///<summary>
	/// 两点间距离
	/// </summary>
	/// <returns>两点间距离
	public float TwoPointsDistance(GameObject obj1 , GameObject obj2)
	{
		var col1 = obj1.GetComponent<Collider> ();
		Vector3 obj1Point = obj1.transform.position;
		obj1Point.x += obj1.transform.forward.normalized.x * col1.bounds.size.x;
		obj1Point.y = 0;
		obj1Point.z += obj1.transform.forward.normalized.z * col1.bounds.size.z;
		var col2 = obj2.GetComponent<Collider> ();
		Vector3 obj2Point = obj2.transform.position;
		obj2Point.x += obj2.transform.forward.normalized.x * col2.bounds.size.x;
		obj2Point.y = 0;
		obj2Point.z += obj2.transform.forward.normalized.z * col2.bounds.size.z;
		float distance = Vector3.Distance (obj1Point , obj2Point);
		return distance;
	}

	///<summary>
	/// 范围内object与选取单位的距离
	/// </summary>
	/// <param name="range">范围
	/// <returns>与选取单位等同数量的距离List
	public Collider[] DistanceList(GameObject obj , float range)
	{
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
		if (cols.Length > 0)
		{
			int i, j = 1;
			Collider select = cols [0];
			while (j < cols.Length) 
			{
				for (i = 0; i < cols.Length - j; i++) 
				{
					float a = Vector3.Distance (obj.transform.position, cols[i].transform.position);
					float b = Vector3.Distance (obj.transform.position, cols[i+1].transform.position);
					if (a > b) 
					{
						select = cols [i];
						cols [i] = cols [i+1];
						cols [i+1] = select;
						break;
					}
				}
				j++;
			}
		}
		return cols;
	}
	//未完成
	public GameObject Straightline(GameObject obj , float distance)
	{
		GameObject target = null;
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, distance, Layer);
		int[] num = new int[cols.Length];
		if (cols.Length > 0) 
		{
			for (int i = 0; i < cols.Length ; i++) 
			{
				Vector3 direction = obj.transform.position - cols[i].transform.position;
				for (int j = 0; j < cols.Length; j++) 
				{
					if (Physics.Raycast (cols[j].transform.position, direction , distance , Layer)) 
					{
						num [i]++;
					}
					j++;
				}
			}
			if (num.Length > 1)
			{
				for (int k = 1; k < num.Length; k++) 
				{
					if (num [k] > num [k - 1])
						target = cols [k].gameObject;
				}
			}
			else
			{
				target = cols [0].gameObject;
			}
		}
		return target;
	}
	//未完成
	public GameObject CircularRange(GameObject obj , float distance , float range)
	{
		GameObject target = null;
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, distance, Layer);
		int[] num = new int[cols.Length];
		if (cols.Length > 0) 
		{
			for (int i = 0; i < cols.Length ; i++) 
			{
				Collider[] col = Physics.OverlapSphere(cols[i].transform.position, range, Layer);
				num [i] = col.Length;
			}
			if (num.Length > 1)
			{
				for (int k = 1; k < num.Length; k++) 
				{
					if (num [k] > num [k - 1])
						target = cols [k].gameObject;
				}
			}
			else
			{
				target = cols [0].gameObject;
			}
		}
		return target;
	}
	/// <summary>
	/// 获取object的命令
	/// </summary>
	/// <returns>命令号码</returns>
	/// <param name="obj">单位</param>
	public int getCommand(GameObject obj)
	{
		int value = 0;
		if (obj.tag.Equals ("Ally")) 
		{
			value = obj.GetComponent<HeroController> ().Command;
		} 
		else if (obj.tag.Equals ("Enemy")) 
		{
			value = obj.GetComponent<EnemyController> ().Command;
		}
		return value;
	}
	/// <summary>
	/// 设置命令
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">命令号码</param>
	public void setCommand(GameObject obj , int value)
	{
		if (obj.tag.Equals ("Ally")) 
		{
			obj.GetComponent<HeroController> ().Command = value;
		} 
		else if (obj.tag.Equals ("Enemy")) 
		{
			obj.GetComponent<EnemyController> ().Command = value;
		}
	}
	/// <summary>
	/// Skills the target.
	/// </summary>
	public bool SkillTarget(GameObject obj , float range)
	{
		bool target = false;
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, 1 << 11);

		return target;
	}
	/// <summary>
	/// 获取单位敌对的层
	/// </summary>
	/// <returns>层的号码</returns>
	/// <param name="obj">单位</param>
	public int getTargetLayer(GameObject obj)
	{
		int Layer = 0;
		if (obj.tag.Equals ("Ally")) 
		{
			Layer = 1 << LayerMask.NameToLayer ("Enemy");
		} 
		else if (obj.tag.Equals ("Enemy")) 
		{
			Layer = 1 << LayerMask.NameToLayer ("Ally");
		}
		return Layer;
	}
	/// <summary>
	/// 获取单位的类型
	/// </summary>
	/// <returns>单位的类型</returns>
	/// <param name="obj">单位</param>
	public string getCharcterType(GameObject obj)
	{
		string value = "";
		if (obj.transform.tag == "Ally") 
		{
			value = obj.GetComponent<HeroController> ().CharacterType;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().CharacterType;
		}
		return value;
	}
	/// <summary>
	/// 获取单位的统领值
	/// </summary>
	/// <returns>统领值</returns>
	/// <param name="obj">单位</param>
	public int getLeaderShip(GameObject obj)
	{
		int value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().LeaderShip;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少单位的统领值
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">数值</param>
	public void setLeaderShip(GameObject obj , int value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				obj.GetComponent<HeroController> ().LeaderShip += value;
		}
	}
	/// <summary>
	/// 获取率领友军的数量
	/// </summary>
	/// <returns>数量</returns>
	/// <param name="obj">单位</param>
	public int getFriendNum(GameObject obj)
	{
		int value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().FriendsNumber[0] + obj.GetComponent<HeroController> ().FriendsNumber[1];
		}
		return value;
	}
	/// <summary>
	/// 获取单位的生命值
	/// </summary>
	/// <returns>生命值</returns>
	/// <param name="obj">单位</param>
	public int getHealthPower(GameObject obj)
	{
		int value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().HealthPower + obj.GetComponent<HeroController> ().HealthPowerAdditional;
			else
				value = obj.GetComponent<FriendController> ().HealthPower + obj.GetComponent<FriendController> ().HealthPowerAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().HealthPower + obj.GetComponent<EnemyController> ().HealthPowerAdditional;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少单位的生命值
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">数值</param>
	public void setHealthPower(GameObject obj , int value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null) 
				obj.GetComponent<HeroController> ().HealthPower += value;
			else 
				value = obj.GetComponent<FriendController> ().HealthPower += value;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().HealthPower += value;
		}
	}
	/// <summary>
	/// 获取生命最大值
	/// </summary>
	/// <returns>最大生命值</returns>
	/// <param name="obj">单位</param>
	public int getHealthPowerMax(GameObject obj)
	{
		int value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().HealthPowerMax;
			else
				value = obj.GetComponent<FriendController> ().HealthPowerMax;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().HealthPowerMax;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少生命最大值
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">数值.</param>
	public void setHealthPowerMax(GameObject obj , int value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null) {
				obj.GetComponent<HeroController> ().HealthPower += value;
				obj.GetComponent<HeroController> ().HealthPowerMax += value;
			}
			else 
			{
				obj.GetComponent<FriendController> ().HealthPower += value;
				obj.GetComponent<FriendController> ().HealthPowerMax += value;
			}
		}
		if (obj.transform.tag == "Enemy") 
		{
			obj.GetComponent<EnemyController> ().HealthPower += value;
			obj.GetComponent<EnemyController> ().HealthPowerMax += value;
		}
	}
	/// <summary>
	/// 重置生命值为最大生命值
	/// </summary>
	/// <param name="obj">单位</param>
	public void resetHealthPower(GameObject obj)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null) {
				obj.GetComponent<HeroController> ().HealthPower = obj.GetComponent<HeroController> ().HealthPowerMax;
			}
			else 
			{
				obj.GetComponent<FriendController> ().HealthPower = obj.GetComponent<FriendController> ().HealthPowerMax;
			}
		}
		if (obj.transform.tag == "Enemy") 
		{
			obj.GetComponent<EnemyController> ().HealthPower = obj.GetComponent<EnemyController> ().HealthPowerMax;
		}
	}
	/// <summary>
	/// 获取单位攻击力
	/// </summary>
	/// <returns>攻击力</returns>
	/// <param name="obj">单位</param>
	public float getAttack(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Attack + obj.GetComponent<HeroController> ().AttackAdditional;
			else
				value = obj.GetComponent<FriendController> ().Attack + obj.GetComponent<FriendController> ().AttackAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Attack + obj.GetComponent<EnemyController> ().AttackAdditional;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少攻击力
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">数值</param>
	public void setAttack(GameObject obj , float value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				obj.GetComponent<HeroController> ().Attack += value;
			else
				value = obj.GetComponent<FriendController> ().Attack += value;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Attack += value;
		}
	}
	/// <summary>
	/// 获取单位防御值
	/// </summary>
	/// <returns>防御值</returns>
	/// <param name="obj">单位</param>
	public float getDefence(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Defence + obj.GetComponent<HeroController> ().DefenceAdditional;
			else
				value = obj.GetComponent<FriendController> ().Defence + obj.GetComponent<FriendController> ().DefenceAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Defence + obj.GetComponent<EnemyController> ().DefenceAdditional;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少防御值
	/// </summary>
	/// <param name="obj">单位制</param>
	/// <param name="value">数值</param>
	public void setDefence(GameObject obj , float value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				obj.GetComponent<HeroController> ().Defence += value;
			else
				value = obj.GetComponent<FriendController> ().Defence += value;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Defence += value;
		}
	}
	/// <summary>
	/// 获取敏捷值
	/// </summary>
	/// <returns>敏捷值</returns>
	/// <param name="obj">单位</param>
	public float getDexterity(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Dexterity + obj.GetComponent<HeroController> ().DexterityAdditional;
			else
				value = obj.GetComponent<FriendController> ().Dexterity + obj.GetComponent<FriendController> ().DexterityAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Dexterity + obj.GetComponent<EnemyController> ().DexterityAdditional;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少敏捷值
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">数值</param>
	public void setDexterity(GameObject obj , float value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				obj.GetComponent<HeroController> ().Dexterity += value;
			else
				value = obj.GetComponent<FriendController> ().Dexterity += value;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Dexterity += value;
		}
	}
	/// <summary>
	/// 获取灵巧值
	/// </summary>
	/// <returns>灵巧值</returns>
	/// <param name="obj">单位</param>
	public float getAgility(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Agility + obj.GetComponent<HeroController> ().AgilityAdditional;
			else
				value = obj.GetComponent<FriendController> ().Agility + obj.GetComponent<FriendController> ().AgilityAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Agility + obj.GetComponent<EnemyController> ().AgilityAdditional;
		}
		return value;
	}
	/// <summary>
	/// 增加或减少灵巧值
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="value">数量</param>
	public void setAgility(GameObject obj , float value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				obj.GetComponent<HeroController> ().Agility += value;
			else
				value = obj.GetComponent<FriendController> ().Agility += value;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Agility += value;
		}
	}
	/// <summary>
	/// 获取命中率
	/// </summary>
	/// <returns>命中率</returns>
	/// <param name="obj">单位</param>
	public float getHit(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Hit + obj.GetComponent<HeroController> ().HitAdditional;
			else
				value = obj.GetComponent<FriendController> ().Hit + obj.GetComponent<FriendController> ().HitAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Hit + obj.GetComponent<EnemyController> ().HitAdditional;
		}
		return value;
	}
	/// <summary>
	/// 获取闪避率
	/// </summary>
	/// <returns>闪避率</returns>
	/// <param name="obj">单位</param>
	public float getDodge(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Dodge + obj.GetComponent<HeroController> ().DodgeAdditional;
			else
				value = obj.GetComponent<FriendController> ().Dodge + obj.GetComponent<FriendController> ().DodgeAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Dodge + obj.GetComponent<EnemyController> ().DodgeAdditional;
		}
		return value;
	}
	/// <summary>
	/// 获取暴击率
	/// </summary>
	/// <returns>暴击率</returns>
	/// <param name="obj">单位</param>
	public float getCritical(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().Critical + obj.GetComponent<HeroController> ().CriticalAdditional;
			else
				value = obj.GetComponent<FriendController> ().Critical + obj.GetComponent<FriendController> ().CriticalAdditional;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().Critical + obj.GetComponent<EnemyController> ().CriticalAdditional;
		}
		return value;
	}
	/// <summary>
	/// 获取视野范围
	/// </summary>
	/// <returns>视野范围</returns>
	/// <param name="obj">单位</param>
	public float getSightRange(GameObject obj)
	{
		float value = 0;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().SightRange;
			else
				value = obj.GetComponent<FriendController> ().SightRange;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().SightRange;
		}
		return value;
	}
	/// <summary>
	/// 获取主动技能
	/// </summary>
	/// <returns>主动技能的List</returns>
	/// <param name="obj">单位</param>
	public ArrayList getActiveSkillSelect(GameObject obj)
	{
		ArrayList value = new ArrayList();
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().ActiveSkillSelect;
			else
				value = obj.GetComponent<FriendController> ().ActiveSkillSelect;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().ActiveSkillSelect;
		}
		return value;
	}
	/// <summary>
	/// 获取被动技能
	/// </summary>
	/// <returns>被动技能的List</returns>
	/// <param name="obj">单位</param>
	public ArrayList getPassiveSkillSelect(GameObject obj)
	{
		ArrayList value = new ArrayList();
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().PassiveSkillSelect;
			else
				value = obj.GetComponent<FriendController> ().PassiveSkillSelect;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().PassiveSkillSelect;
		}
		return value;
	}
	/// <summary>
	/// 获取单位的攻击目标
	/// </summary>
	/// <returns>攻击目标</returns>
	/// <param name="obj">单位</param>
	public GameObject getAttactTarget(GameObject obj)
	{
		GameObject value = null;
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				value = obj.GetComponent<HeroController> ().AttackTarget;
			else
				value = obj.GetComponent<FriendController> ().AttackTarget;
		}
		if (obj.transform.tag == "Enemy") 
		{
			value = obj.GetComponent<EnemyController> ().AttackTarget;
		}
		return value;
	}
	/// <summary>
	/// 设置单位的攻击目标
	/// </summary>
	/// <param name="obj">单位</param>
	/// <param name="target">目标</param>
	public void setAttactTarget(GameObject obj , GameObject target)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (target.GetComponent<HeroController> () != null)
				target.GetComponent<HeroController> ().AttackTarget = obj.GetComponent<HeroController> ().AttackTarget;
			else
				target.GetComponent<FriendController> ().AttackTarget = obj.GetComponent<HeroController> ().AttackTarget;
		}
		if (obj.transform.tag == "Enemy") 
		{
			target.GetComponent<EnemyController> ().AttackTarget = obj.GetComponent<EnemyController> ().AttackTarget;
		}
	}
	/// <summary>
	/// 获取Ally的动画
	/// </summary>
	/// <returns>动画参数</returns>
	/// <param name="obj">单位</param>
	public Animator getAllyAnimator(GameObject obj)
	{
		Animator animator = new Animator();
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				animator = obj.GetComponent<HeroController> ().HeroAnimator;
			else
				animator = obj.GetComponent<FriendController> ().FriendAnimator;
		}
		return animator;
	}
	/// <summary>
	/// 获取Enemy的动画
	/// </summary>
	/// <returns>动画参数</returns>
	/// <param name="obj">单位</param>
	public Animation getEnemyAnimator(GameObject obj)
	{
		Animation animation = new Animation();
		if (obj.transform.tag == "Enemy") 
			animation = obj.GetComponent<EnemyController> ().EnemyAnimator;
		return animation;
	}
	/// <summary>
	/// 获取NavmeshAgent
	/// </summary>
	/// <returns>导航参数</returns>
	/// <param name="obj">单位</param>
	public NavMeshAgent getNavmeshAgent(GameObject obj)
	{
		NavMeshAgent agent = new NavMeshAgent ();
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null) 
				agent = obj.GetComponent<HeroController> ().HeroAgent;
			else 
				agent = obj.GetComponent<FriendController> ().FriendAgent;
			
		}
		if (obj.transform.tag == "Enemy") 
			agent = obj.GetComponent<EnemyController> ().EnemyAgent;
		return agent;
	}
}
