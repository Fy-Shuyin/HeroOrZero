using UnityEngine;
using System.Collections;

public class HeroDeath : HeroIState
{
	public void Enter(HeroController Hero)
	{
		Hero.HeroAnimator.SetTrigger ("Death");
	}

	public void Execute(HeroController Hero)
	{}

	public void Exit(HeroController Hero)
	{}
}
