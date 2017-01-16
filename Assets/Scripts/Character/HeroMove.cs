using UnityEngine;
using System.Collections;

public class HeroMove : HeroIState
{
	public void Enter(HeroController Hero)
	{
		Hero.HeroAnimator.SetFloat ("Move",1);
	}

	public void Execute(HeroController Hero)
	{
		if (!Hero.IsLife) 
		{
			Hero.ChangeState (new HeroDeath ());
			return;
		}
		if (Vector3.Distance(Hero.transform.position,Hero.TouchPosition) < 2) 
		{
			Hero.ChangeState (new HeroIdle ());
			return;
		} 
		Hero.HeroAgent.Resume();
		Hero.HeroAgent.SetDestination(Hero.TouchPosition);
	}

	public void Exit(HeroController Hero)
	{
		Hero.HeroAnimator.SetFloat ("Move",0);
		Hero.HeroAgent.Stop ();
		Hero.HeroAgent.velocity = Vector3.zero;
		Hero.Target ();
	}
}