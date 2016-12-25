using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;

public class SceneBattle : MonoBehaviour {

	GameSystem gamesystem;

	public GameObject Battle_Camera;
	bool Battle_CameraOn;

	public GUISkin BattleScenceGUI;

	public GameObject Hero_One;

	public string EnemyLoadName;
	public int EnemyLoadNum;
	float LoadRepeat;
	bool cameraON; 
	Vector3[] point;

	void Awake()
	{
		Battle_Camera = GameObject.Find ("Camera"); 

		point = new Vector3[4];
		point [0] = GameObject.Find ("StartPosition").transform.position;
		point [1] = GameObject.Find ("StandbyPosition (1)").transform.position;
		point [2] = GameObject.Find ("StandbyPosition (2)").transform.position;
		point [3] = GameObject.Find ("StandbyPosition (3)").transform.position;

		EnemyLoadName = "Enemy_Wolf";
		EnemyLoadNum = 7;
	}

	void Start ()
	{
		var prefab = Resources.Load ("Heros/Swordman");
		Hero_One = Instantiate (prefab) as GameObject;
		Hero_One.transform.position = point [0];
		Hero_One.GetComponent<HeroController> ().HeroAgent.SetDestination (point [1]);
	}

	void Update () 
	{
		LoadRepeat -= Time.deltaTime;
		if (LoadRepeat < 0) 
		{
			LoadRepeat = 0;
		}
		if (LoadRepeat == 0 && EnemyLoadNum != 0) 
		{
			EnemyLoad ();
			LoadRepeat = 1f;
		}
		CameraOn ();
	}

	void CameraOn()
	{
		if (Battle_CameraOn == true)
		{
			Battle_Camera.transform.position = new Vector3(Hero_One.transform.position.x,25f,Hero_One.transform.position.z-15);
		} else 
		{
			Battle_Camera.transform.position = new Vector3 (0,60,-45);
		}
	}
	void EnemyLoad()
	{
		var prefab = Resources.Load("Enemies/" + EnemyLoadName);
		var enemy = Instantiate(prefab) as GameObject;
		float x = Random.Range(-25f, 25f);
		float z = 60f;
		enemy.transform.position = new Vector3(x, 1f,z);
		EnemyLoadNum--;
	}

	void OnButtonClick()
	{ 
		if (Battle_CameraOn == true) 
		{
			Battle_CameraOn = false;
			Hero_One.GetComponent<HeroController> ().setAIMode (true);
		} else if (Battle_CameraOn == false) 
		{
			Battle_CameraOn = true;
			Hero_One.GetComponent<HeroController> ().setAIMode (false);
		}
	}
}
