using UnityEngine;
using System.Collections;

public class EnemySoldierAttack : EnemyIState
{
	public void Enter(EnemyController Enemy)
	{
		Enemy.transform.LookAt (Enemy.AttackTarget.transform);
		Enemy.EnemyAnimator.CrossFade ("Attack");
		Enemy.runAttack ();
		Enemy.AttackCooldown = Enemy.AttackSpeed;
		Enemy.ChangeState (new EnemySoldierThink ());
		return;
	}

	public void Execute(EnemyController Enemy)
	{}

	public void Exit(EnemyController Enemy)
	{}
}
