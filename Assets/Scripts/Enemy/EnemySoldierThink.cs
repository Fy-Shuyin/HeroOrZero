using UnityEngine;
using System.Collections;

public class EnemySoldierThink :  EnemyIState
{
	Vector3 EnemyVector, TargetVector;
	public void Enter(EnemyController Enemy)
	{}

	public void Execute(EnemyController Enemy)
	{
		if (!Enemy.IsLife) 
		{
			Enemy.ChangeState (new EnemyDeath ());
			return;
		}
		if (Enemy.AttackTarget == Enemy.TargetEGG) 
		{
			Enemy.Target ();
		}
		if (Enemy.AttackTarget == null || !Enemy.Patterns.TargetIsLife(Enemy.AttackTarget)) 
		{
			Enemy.AttackTarget = Enemy.TargetEGG;
			Enemy.ChangeState (new EnemyMove ());
			return;
		}
		EnemyVector = Enemy.gameObject.transform.position;
		TargetVector = Enemy.AttackTarget.transform.position;
		if (Vector3.Distance (EnemyVector, TargetVector) < Enemy.AttackRange) 
		{
			if (Enemy.AttackCooldown == 0) 
			{
				Enemy.ChangeState (new EnemySoldierAttack ());
				return;
			} 
			else 
			{
				Enemy.AttackCooldown -= Time.deltaTime;
				if (Enemy.AttackCooldown < 0) {
					Enemy.AttackCooldown = 0;
				}
			}
		}
		else
		{
			Enemy.ChangeState (new EnemyMove ());
			return;
		}

	}

	public void Exit(EnemyController Enemy)
	{}
}
