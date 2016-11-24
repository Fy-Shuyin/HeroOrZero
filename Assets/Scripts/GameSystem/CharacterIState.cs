using UnityEngine;
using System.Collections;

public interface HeroIState 
{
	//进入状态是只呼出1次
	void Enter(HeroController State);

	//每针呼出状态
	void Execute(HeroController State);

	//离开状态时呼出1次
	void Exit(HeroController State);
}

public interface FriendIState 
{
	//进入状态是只呼出1次
	void Enter(FriendController State);

	//每针呼出状态
	void Execute(FriendController State);

	//离开状态时呼出1次
	void Exit(FriendController State);
}

public interface EnemyIState 
{
	//进入状态是只呼出1次
	void Enter(EnemyController State);

	//每针呼出状态
	void Execute(EnemyController State);

	//离开状态时呼出1次
	void Exit(EnemyController State);
}
