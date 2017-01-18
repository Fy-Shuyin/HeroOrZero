using UnityEngine;
using System.Collections;

public class EnemySoldierThink :  EnemyIState
{
	Vector3 EnemyVector, TargetVector;
	public void Enter(EnemyController Enemy)
	{}

	public void Execute(EnemyController Enemy)
	{
		if (!Enemy.IsLive) 
		{
			Enemy.ChangeState (new EnemyDeath ());
			return;
		}
		if (Enemy.AttackTarget == Enemy.TargetEGG) 
		{
			stateToAttact (Enemy, 18f);
			Enemy.Target ();
		}
		else if (Enemy.AttackTarget == null || !Enemy.Patterns.TargetIsLive (Enemy.AttackTarget)) 
		{
			Enemy.AttackTarget = Enemy.TargetEGG;
			Enemy.ChangeState (new EnemyMove ());
			return;
		}
		else 
		{
			stateToAttact (Enemy, Enemy.AttackRange);
		}
	}

	public void Exit(EnemyController Enemy)
	{}

	public void stateToAttact(EnemyController Enemy , float attackRange)
	{
		EnemyVector = Enemy.gameObject.transform.position;
		TargetVector = Enemy.AttackTarget.transform.position;
		if (Vector3.Distance (EnemyVector, TargetVector) < attackRange) 
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
}
