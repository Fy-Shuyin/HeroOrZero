using UnityEngine;
using System.Collections;

public class HeroSpellSkills : HeroIState
{
	float timer = 0;

	public void Enter(HeroController Hero)
	{
		Hero.Skills.SkillSpell (Hero.gameObject, Hero.SpellSkillName);
		timer = 1;
	}

	public void Execute(HeroController Hero)
	{
		timer -= Time.deltaTime;
		if (timer < 0) 
		{
			Hero.ChangeState (new HeroThink ());
			return;
		}
	}

	public void Exit(HeroController Hero)
	{
		Hero.SpellSkillName = "";
	}
}
