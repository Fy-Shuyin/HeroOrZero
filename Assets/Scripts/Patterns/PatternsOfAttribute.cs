using UnityEngine;
using System.Collections;

public class PatternsOfAttribute
{

	public bool TargetIsLive(GameObject target)
	{
		bool isLife = true;
		if (getHealthPower (target) <= 0) 
		{
			isLife = false;
		}
		return isLife;
	}

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

	public int NumberOfAllies(GameObject obj , float range)
	{
		string Tag = obj.gameObject.tag;
		int Layer = 1 << LayerMask.NameToLayer (Tag);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
		return cols.Length - 1;
	}

	public ArrayList AlliesFriend(GameObject obj , float range)
	{
		ArrayList alliesList = new ArrayList ();
		string Tag = obj.gameObject.tag;
		int Layer = 1 << LayerMask.NameToLayer (Tag);
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

	public int NumberOfEnemies()
	{
		int Layer = 1 << LayerMask.NameToLayer ("Enemy");;
		Collider[] cols = Physics.OverlapSphere(new Vector3() , 500f , Layer);
		return cols.Length;
	}

	public int NumberOfEnemies(GameObject obj , float range)
	{
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
		return cols.Length;
	}

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

	public Collider[] BloodVolume(GameObject obj , float distance)
	{
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, distance, Layer);
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

	public Collider[] Distance(GameObject obj , float distance)
	{
		int Layer = getTargetLayer(obj);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, distance, Layer);
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

	public bool SkillTarget(GameObject obj , float distance)
	{
		bool target = false;
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, distance, 1 << 11);

		return target;
	}

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

	public void setLeaderShip(GameObject obj , int value)
	{
		if (obj.transform.tag == "Ally") 
		{
			if (obj.GetComponent<HeroController> () != null)
				obj.GetComponent<HeroController> ().LeaderShip += value;
		}
	}

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

	public Animation getEnemyAnimator(GameObject obj)
	{
		Animation animation = new Animation();
		if (obj.transform.tag == "Enemy") 
		{
			animation = obj.GetComponent<EnemyController> ().EnemyAnimator;
		}
		return animation;
	}
}
