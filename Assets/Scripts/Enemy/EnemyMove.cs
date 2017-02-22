using UnityEngine;
using System.Collections;

public class EnemyMove : EnemyIState
{
	Vector3 EnemyVector, TargetVector;

	public void Enter(EnemyController Enemy)
	{
		Enemy.EnemyAnimator.CrossFade ("Running");
	}

	public void Execute(EnemyController Enemy)
	{
		if (!Enemy.IsLive) 
		{
			Enemy.ChangeState (new EnemyDeath ());
			return;
		}
		if (Enemy.AttackTarget == Enemy.TargetEGG)
		{
			Enemy.Target ();
			stateToThink (Enemy, 15f);
		} 
		else if (!Enemy.Patterns.TargetIsLive (Enemy.AttackTarget)) 
		{
			Enemy.Target ();
		} 
		else 
		{
			stateToThink (Enemy, Enemy.AttackRange);
		}

	}

	public void Exit(EnemyController Enemy)
	{
		Enemy.EnemyAgent.Stop ();
		Enemy.EnemyAgent.velocity = Vector3.zero;
	}

	private void stateToThink(EnemyController Enemy , float attackRange)
	{
		EnemyVector = Enemy.gameObject.transform.position;
		TargetVector = Enemy.AttackTarget.transform.position;
		EnemyVector.y = 0;
		TargetVector.y = 0;
		if (Vector3.Distance (EnemyVector, TargetVector) < attackRange) 
		{
			Enemy.ChangeState (new EnemySoldierThink());
			return;
		}
		Enemy.Move (TargetVector);
	}
}
