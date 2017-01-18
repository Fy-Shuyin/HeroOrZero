using UnityEngine;
using System.Collections;

public class HeroChase : HeroIState
{
	Vector3 HeroVector, TargetVector;

	public void Enter(HeroController Hero)
	{
		Hero.HeroAnimator.SetFloat ("Move",1);
	}

	public void Execute(HeroController Hero)
	{
		if (!Hero.IsLive) 
		{
			Hero.ChangeState (new HeroDeath ());
			return;
		}
		if (Hero.AttackTarget == null) 
		{
			Hero.ChangeState (new HeroIdle ());
			return;
		}
		if (!Hero.Patterns.TargetIsLive (Hero.AttackTarget)) 
		{
			Hero.AttackTarget = null;
			return;
		}
		HeroVector = Hero.gameObject.transform.position;
		TargetVector = Hero.AttackTarget.transform.position;
		if (Vector3.Distance (HeroVector, TargetVector) < Hero.AttackRange) 
		{
			Hero.ChangeState (new HeroAttack());
			return;
		}
		if (Vector3.Distance (HeroVector, TargetVector) > Hero.SightRange) 
		{
			Hero.ChangeState (new HeroIdle());
			return;
		}
		Hero.HeroAgent.Resume();
		Hero.HeroAgent.SetDestination(TargetVector);
	}

	public void Exit(HeroController Hero)
	{
		Hero.HeroAnimator.SetFloat ("Move",0);
		Hero.HeroAgent.Stop ();
		Hero.HeroAgent.velocity = Vector3.zero;
	}
}