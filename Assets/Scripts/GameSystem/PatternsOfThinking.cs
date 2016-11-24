using UnityEngine;
using System.Collections;

public class PatternsOfThinking {

	public bool TargetIsLife(GameObject target)
	{
		bool isLife = true;
		if (getHealthPower (target) <= 0) 
		{
			isLife = false;
		}
		return isLife;
	}

	public void NumberOfSkill(ArrayList sklls)
	{
	}

	public int NumberOfAllies(GameObject obj , float range)
	{
		string Tag = obj.gameObject.tag;
		int Layer = 1 << LayerMask.NameToLayer (Tag);
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, range, Layer);
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

	public bool SkillTarget(GameObject obj , float distance)
	{
		bool target = false;
		Collider[] cols = Physics.OverlapSphere(obj.transform.position, distance, 1 << 11);

		return target;
	}

	int getTargetLayer(GameObject obj)
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

	float getHealthPower(GameObject obj)
	{
		float value = 0;
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

	float getAttack(GameObject obj)
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

	float getDefence(GameObject obj)
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

	float getHit(GameObject obj)
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

	float getDodge(GameObject obj)
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


}
