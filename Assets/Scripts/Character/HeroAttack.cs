using UnityEngine;
using System.Collections;

public class HeroAttack : HeroIState
{
	public void Enter(HeroController Hero)
	{
		Hero.transform.LookAt (Hero.AttackTarget.transform);
		Hero.HeroAnimator.SetTrigger ("Attack");
		Hero.AttackCooldown = Hero.AttackSpeed;
		Hero.runAttack ();
		Hero.ChangeState (new HeroThink ());
		return;
	}

	public void Execute(HeroController Hero)
	{}

	public void Exit(HeroController Hero)
	{}
}