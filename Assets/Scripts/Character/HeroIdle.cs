using UnityEngine;
using System.Collections;

public class HeroIdle : HeroIState
{
	Vector3 HeroVector, TargetVector;
	public void Enter(HeroController Hero)
	{
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
			Hero.Target ();
		} 
		else 
		{
			HeroVector = Hero.gameObject.transform.position;
			TargetVector = Hero.AttackTarget.transform.position;
			if(Vector3.Distance (HeroVector, TargetVector) < Hero.SightRange)
			Hero.ChangeState (new HeroChase ());
			return;
		}
	}

	public void Exit(HeroController Hero)
	{}
}
