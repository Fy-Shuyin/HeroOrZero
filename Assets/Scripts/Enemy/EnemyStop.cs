using UnityEngine;
using System.Collections;

public class EnemyStop :  EnemyIState
{
	public float cooldown;
	public void Enter(EnemyController Enemy)
	{
		cooldown = Enemy.StopTime;
	}

	public void Execute(EnemyController Enemy)
	{
		if (!Enemy.IsLive) 
		{
			Enemy.ChangeState (new EnemyDeath ());
			return;
		}
		cooldown -= Time.deltaTime;
		if (cooldown <= 0) 
		{
			Enemy.ChangeState (new EnemyMove ());
			return;
		}
	}

	public void Exit(EnemyController Enemy)
	{}
}
