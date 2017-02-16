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
		if (!Hero.IsLive) 
		{
			Hero.ChangeState (new HeroDeath ());
			return;
		}
		Vector3 heroPos = Hero.transform.position;
		heroPos.y = 0;
		if (Vector3.Distance(heroPos,Hero.TouchPosition) < 3) 
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
	}
}