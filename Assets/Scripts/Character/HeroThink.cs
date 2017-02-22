using UnityEngine;
using System.Collections;

public class HeroThink : HeroIState
{
	Vector3 HeroVector, TargetVector;
	public void Enter(HeroController Hero)
	{
		if (Hero.AImode) 
		{
			Hero.Target ();
		}
	}

	public void Execute(HeroController Hero)
	{
		if (!Hero.IsLive) 
		{
			Hero.ChangeState (new HeroDeath ());
			return;
		}
		if (Hero.AttackTarget == null || !Hero.Patterns.TargetIsLive (Hero.AttackTarget)) 
		{
			Hero.ChangeState (new HeroIdle ());
			return;
		}
		else
		{
			/*if (Hero.AImode) 
			{
				if (Hero.Think ()) 
				{
					return;
				}
			}*/
			HeroVector = Hero.gameObject.transform.position;
			TargetVector = Hero.AttackTarget.transform.position;
			if (Vector3.Distance (HeroVector, TargetVector) < Hero.AttackRange) 
			{
				if (Hero.AttackCooldown == 0) 
				{
					Hero.ChangeState (new HeroAttack ());
					return;
				} 
				else 
				{
					Hero.AttackCooldown -= Time.deltaTime;
					if (Hero.AttackCooldown < 0) {
						Hero.AttackCooldown = 0;
					}
				}
			}
			else
			{
				Hero.ChangeState (new HeroChase ());
				return;
			}
		}
	}

	public void Exit(HeroController Hero)
	{}
}