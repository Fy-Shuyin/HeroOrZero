using UnityEngine;
using System.Collections;

public class HeroStop : HeroIState 
{
	public float cooldown;
	public void Enter(HeroController Hero)
	{
		cooldown = Hero.StopTime;
	}

	public void Execute(HeroController Hero)
	{
		if (!Hero.IsLive) 
		{
			Hero.ChangeState (new HeroDeath ());
			return;
		}
		cooldown -= Time.deltaTime;
		if (cooldown <= 0) 
		{
			Hero.ChangeState (new HeroIdle ());
			return;
		}
	}

	public void Exit(HeroController Hero)
	{}
}
