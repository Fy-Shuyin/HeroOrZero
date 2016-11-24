using UnityEngine;
using System.Collections;

public class HeroIdle : HeroIState
{
	public void Enter(HeroController Hero)
	{
	}

	public void Execute(HeroController Hero)
	{
		if (!Hero.IsLife) 
		{
			Hero.ChangeState (new HeroDeath ());
			return;
		}
		if (Hero.AttackTarget == null) 
		{
			Hero.Target ();
		} 
		else 
		{
			Hero.ChangeState (new HeroChase ());
			return;
		}
	}

	public void Exit(HeroController Hero)
	{}
}
