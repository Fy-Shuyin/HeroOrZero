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
		if (Enemy.AttackTarget == Enemy.TargetEGG) 
		{
			Enemy.Target ();
		} else if (!Enemy.Patterns.TargetIsLife(Enemy.AttackTarget)) 
		{
			Enemy.Target ();
		}
		EnemyVector = Enemy.gameObject.transform.position;
		TargetVector = Enemy.AttackTarget.transform.position;
		if (Vector3.Distance (EnemyVector, TargetVector) < Enemy.AttackRange) 
		{
			Enemy.ChangeState (new EnemySoldierThink());
			return;
		}
		Enemy.Move (TargetVector);
	}

	public void Exit(EnemyController Enemy)
	{
		Enemy.EnemyAgent.Stop ();
		Enemy.EnemyAgent.velocity = Vector3.zero;
	}
}
