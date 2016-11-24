using UnityEngine;
using System.Collections;

public class EnemyDeath : EnemyIState
{
	public void Enter(EnemyController Enemy)
	{
		Enemy.EnemyAnimator.CrossFade ("Death");
	}

	public void Execute(EnemyController Enemy)
	{}

	public void Exit(EnemyController Enemy)
	{}
}
