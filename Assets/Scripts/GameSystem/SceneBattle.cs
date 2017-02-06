using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;

public class SceneBattle : MonoBehaviour {

	GameSystem gameSystem = new GameSystem();
	PatternsOfAttribute Patterns = new PatternsOfAttribute();
	PatternsOfSkills Skills = new PatternsOfSkills();

	public GameObject Battle_Camera;
	bool Battle_CameraOn;
	public GameObject Battle_Camera_Target;
	public GameObject UI_Target;

	public GameObject DefensiveTownHall;

	public GameObject Hero_One_Icon;
	public GameObject Hero_Two_Icon;
	public GameObject Hero_Three_Icon;

	public GameObject UI_Hero_All_Status;
	public GameObject UI_Hero_All_One;
	public RawImage UI_Hero_All_Icon_One;
	public Slider UI_Hero_All_Hp_One;
	public Slider UI_Hero_All_MilitaryStrength_One;
	public GameObject UI_Hero_All_Two;
	public RawImage UI_Hero_All_Icon_Two;
	public Slider UI_Hero_All_Hp_Two;
	public Slider UI_Hero_All_MilitaryStrength_Two;
	public GameObject UI_Hero_All_Three;
	public RawImage UI_Hero_All_Icon_Three;
	public Slider UI_Hero_All_Hp_Three;
	public Slider UI_Hero_All_MilitaryStrength_Three;

	public GameObject UI_Hero_One_Status;
	public Slider UI_Hero_One_HP;
	public Image UI_Hero_One_HP_Bar;
	public RawImage UI_Hero_One_AttIcon;
	public Text UI_Hero_One_AttIcon_Value;
	public RawImage UI_Hero_One_DefIcon;
	public Text UI_Hero_One_DefIcon_Value;
	public RawImage UI_Hero_One_DexIcon;
	public Text UI_Hero_One_DexIcon_Value;
	public RawImage UI_Hero_One_AgiIcon;
	public Text UI_Hero_One_AgiIcon_Value;

	public Button UI_Hero_One_Skill_Active1;
	public RawImage UI_Hero_One_Skill_Active1_Texture;
	public Button UI_Hero_One_Skill_Active2;
	public RawImage UI_Hero_One_Skill_Active2_Texture;
	public Button UI_Hero_One_Skill_Active3;
	public RawImage UI_Hero_One_Skill_Active3_Texture;
	public Image UI_Hero_One_Skill_Passive1;
	public RawImage UI_Hero_One_Skill_Passive1_Texture;
	public Image UI_Hero_One_Skill_Passive2;
	public RawImage UI_Hero_One_Skill_Passive2_Texture;

	public Button UI_Order_Concentrate;
	public Button UI_Order_Disperse;
	public Button UI_Order_Standby;

	public GameObject Hero_One;
	public GameObject Hero_Two;
	public GameObject Hero_Three;

	public int StageLevel;
	private float StageStartCountDown;
	private float StageTime;
	private ArrayList HeroList;
	private ArrayList EnemyList;
	public string EnemyLoadName;
	public int EnemyLoadNum;
	private float LoadRepeat;
	private bool cameraON; 
	private Vector3[] point;

	void Awake()
	{
		Battle_Camera = GameObject.Find ("Camera"); 

		UI_Hero_All_Status = GameObject.Find ("AllStatusPanel");
		UI_Hero_One_Status = GameObject.Find ("StatusPanel");
		UI_Hero_One_Status.SetActive (false);

		Hero_One_Icon = GameObject.Find ("Hero_One_Icon");
		Hero_One_Icon.SetActive (false);
		Hero_Two_Icon = GameObject.Find ("Hero_Two_Icon");
		Hero_Two_Icon.SetActive (false);
		Hero_Three_Icon = GameObject.Find ("Hero_Three_Icon");
		Hero_Three_Icon.SetActive (false);

		DefensiveTownHall = GameObject.Find ("TownHall");

		point = new Vector3[4];
		point [0] = GameObject.Find ("StartPosition").transform.position;
		point [1] = GameObject.Find ("StandbyPosition (1)").transform.position;
		point [2] = GameObject.Find ("StandbyPosition (2)").transform.position;
		point [3] = GameObject.Find ("StandbyPosition (3)").transform.position;

		StageLevel = 10; 
	}

	void Start ()
	{
		StageStartCountDown = 3;
		StageTime = 0;
		NextStage ();
	}

	void Update () 
	{
		StageStartCountDown -= Time.deltaTime;
		if (StageStartCountDown <= 0)
			StageStartCountDown = 0;
		if (StageStartCountDown == 0) 
		{
			StageTime += Time.deltaTime;
			if (StageTime >= 10f) 
			{
				StageListen ();
			}
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
		}

		CameraOn ();
		isHeroLive ();
		HeroAllStatusUI (Hero_One, Hero_Two, Hero_Three);
		HeroOneStatusUI (Battle_Camera_Target);	
		GameOver ();
	}

	void StageListen()
	{
		int count = Patterns.NumberOfEnemies ();
		if (count == 0 && EnemyLoadNum == 0) 
		{
			StageStartCountDown = 3;
			StageTime = 0;
			NextStage ();
		}
	}

	void NextStage()
	{
		StageLevel++;
		if (StageLevel == 16) 
		{
			StartCoroutine(GameOver(5f , 3));
		}
		gameSystem.BattleStart (StageLevel , ref HeroList , ref EnemyList);
		setEnemy (EnemyList);
		if (StageLevel == 1 || StageLevel == 6 || StageLevel == 11 || StageLevel == 15) 
		{
			setHero (HeroList);
		}
	}

	void GameOver()
	{
		var townHallAtt = DefensiveTownHall.GetComponent<TownHallAttribute> ();
		if (!townHallAtt.isLife) 
		{
			//GameOver
			StartCoroutine(GameOver(5f , 4));
		}
	}

	IEnumerator GameOver(float waitTime , int loadNum)
	{
		yield return new WaitForSeconds(waitTime);
		LoadStage.Globe.loadName = loadNum;
		Application.LoadLevel(0);
	}

	void CameraOn()
	{
		if (Battle_CameraOn)
		{
			Battle_Camera.transform.position = new Vector3(Battle_Camera_Target.transform.position.x,30f,Battle_Camera_Target.transform.position.z-5);
		} else 
		{
			Battle_Camera.transform.position = new Vector3 (0,90,-55);
		}
	}

	void setHero(ArrayList heroList)
	{
		for (int i = 0; i < heroList.Count; i++) 
		{
			HeroLoad (heroList[i].ToString() , i);
		}
	}

	void HeroLoad(string heroName , int num)
	{
		if (num == 0) 
		{
			if (Hero_One != null) 
			{
				if (Patterns.TargetIsLive (Hero_One)) 
				{
					Patterns.resetHealthPower(Hero_One);
				} 
				else 
				{
					Patterns.resetHealthPower(Hero_One);
					Patterns.setTargetLive (Hero_One);
					Hero_One.GetComponent<HeroController> ().ChangeToMoveStage (point[1]);
					Hero_One.GetComponent<Collider> ().enabled = true;
					Hero_One_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
						OnButtonClick (Hero_One);
					});
				}
			} 
			else 
			{
				var prefab_One = Resources.Load ("Heros/"+ heroName);
				Hero_One = Instantiate (prefab_One) as GameObject;
				Hero_One.AddComponent<HeroController> ();
				Hero_One.transform.position = point [1];
				Hero_One_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
					OnButtonClick (Hero_One);
				});
				string iconPath = ("Icon/Icon_" + Hero_One.gameObject.name).Replace ("(Clone)", ""); 
				Hero_One_Icon.GetComponentInChildren<RawImage> ().texture = (Texture)Resources.Load (iconPath);
				Hero_One_Icon.SetActive (true);
			}
		}

		if (num == 1) 
		{
			if (Hero_Two != null) 
			{
				if (Patterns.TargetIsLive (Hero_Two)) 
				{
					Patterns.resetHealthPower(Hero_Two);
				} 
				else 
				{
					Patterns.resetHealthPower(Hero_Two);
					Patterns.setTargetLive (Hero_Two);
					Hero_Two.GetComponent<HeroController> ().ChangeToMoveStage (point[2]);
					Hero_Two.GetComponent<Collider> ().enabled = true;
					Hero_Two_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
						OnButtonClick (Hero_Two);
					});
				}
			} 
			else 
			{
				var prefab_Two = Resources.Load ("Heros/"+ heroName);
				Hero_Two = Instantiate (prefab_Two) as GameObject;
				Hero_Two.AddComponent<HeroController> ();
				Hero_Two.transform.position = point [2];
				Hero_Two_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
					OnButtonClick (Hero_Two);
				});
				string iconPath = ("Icon/Icon_" + Hero_Two.gameObject.name).Replace ("(Clone)", ""); 
				Hero_Two_Icon.GetComponentInChildren<RawImage> ().texture = (Texture)Resources.Load (iconPath);
				Hero_Two_Icon.SetActive (true);
			}
		}

		if (num == 2) 
		{
			if (Hero_Three != null) 
			{
				if (Patterns.TargetIsLive (Hero_Three)) 
				{
					Patterns.resetHealthPower(Hero_Three);
				} 
				else 
				{
					Patterns.resetHealthPower(Hero_Three);
					Patterns.setTargetLive (Hero_Three);
					Hero_Three.GetComponent<HeroController> ().ChangeToMoveStage (point[3]);
					Hero_Three.GetComponent<Collider> ().enabled = true;
					Hero_Three_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
						OnButtonClick (Hero_Three);
					});
				}
			} 
			else 
			{
				var prefab_Three = Resources.Load ("Heros/"+ heroName);
				Hero_Three = Instantiate (prefab_Three) as GameObject;
				Hero_Three.AddComponent<HeroController> ();
				Hero_Three.transform.position = point [3];
				Hero_Three_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
					OnButtonClick (Hero_Three);
				});
				string iconPath = ("Icon/Icon_" + Hero_Three.gameObject.name).Replace ("(Clone)", ""); 
				Hero_Three_Icon.GetComponentInChildren<RawImage> ().texture = (Texture)Resources.Load (iconPath);
				Hero_Three_Icon.SetActive (true);
			}
		}
	}

	void setEnemy(ArrayList enemyList)
	{
		LoadRepeat = 2;
		for (int i = 0; i < enemyList.Count; i++) 
		{
			ArrayList list = (ArrayList)enemyList [i];
			EnemyLoadName = list[0].ToString();
			EnemyLoadNum = (int)list[1];
			//EnemyLoadNum = 2;
		}
	}

	void EnemyLoad()
	{
		var prefab = Resources.Load("Enemies/" + EnemyLoadName);
		var enemy = Instantiate(prefab) as GameObject;
		enemy.AddComponent<EnemyController> ();
		float x = Random.Range(-25f, 25f);
		float z = 60f;
		enemy.transform.position = new Vector3(x, 1f,z);
		EnemyLoadNum--;
	}

	void isHeroLive()
	{
		if (Hero_One != null) 
		{
			if (!Patterns.TargetIsLive(Hero_One)) 
			{
				Hero_One_Icon.GetComponent<Button> ().onClick.RemoveAllListeners ();
				if (Battle_CameraOn && Battle_Camera_Target.Equals(Hero_One)) 
				{
					Battle_CameraOn = false;
					setBattleCameraTargetandGUITarget (null);
				}
			}
		}

		if (Hero_Two != null) 
		{
			if (!Patterns.TargetIsLive(Hero_Two)) 
			{
				Hero_Two_Icon.GetComponent<Button> ().onClick.RemoveAllListeners ();
				if (Battle_CameraOn && Battle_Camera_Target.Equals(Hero_Two)) 
				{
					Battle_CameraOn = false;
					setBattleCameraTargetandGUITarget (null);
				}
			}
		}

		if (Hero_Three != null) 
		{
			if (!Patterns.TargetIsLive(Hero_Three)) 
			{
				Hero_Three_Icon.GetComponent<Button> ().onClick.RemoveAllListeners ();
				if (Battle_CameraOn && Battle_Camera_Target.Equals(Hero_Three)) 
				{
					Battle_CameraOn = false;
					setBattleCameraTargetandGUITarget (null);
				}
			}
		}
	}

	void OnButtonClick(GameObject hero)
	{ 
		if (Battle_CameraOn) 
		{
			if (Battle_Camera_Target != null && Battle_Camera_Target != hero) 
			{
				Battle_Camera_Target.GetComponent<HeroController> ().setAIMode (true);
				hero.GetComponent<HeroController> ().setAIMode (false);
				setBattleCameraTargetandGUITarget (hero);
			}
			else
			{
				Battle_CameraOn = false;
				hero.GetComponent<HeroController> ().setAIMode (true);
				setBattleCameraTargetandGUITarget (null);
			}
		} 
		else if (!Battle_CameraOn) 
		{
			Battle_CameraOn = true;
			hero.GetComponent<HeroController> ().setAIMode (false);
			setBattleCameraTargetandGUITarget (hero);
		}
	}

	void setBattleCameraTargetandGUITarget(GameObject target)
	{
		Battle_Camera_Target = target;
		if (target == null) 
		{
			UI_Hero_All_Status.SetActive (true);
			UI_Hero_One_Status.SetActive (false);
		}
		else 
		{
			UI_Hero_All_Status.SetActive (false);
			UI_Hero_One_Status.SetActive (true);
		}
	}

	void HeroAllStatusUI(GameObject target1 , GameObject target2 , GameObject target3)
	{
		if (target1 == null) 
		{
			UI_Hero_All_One.SetActive (false);
		}
		else
		{
			HeroAllStatusTexture (target1, 1);
		}
		if (target2 == null) 
		{
			UI_Hero_All_Two.SetActive (false);
		}
		else
		{
			HeroAllStatusTexture (target2, 2);
		}
		if (target3 == null) 
		{
			UI_Hero_All_Three.SetActive (false);
		}
		else
		{
			HeroAllStatusTexture (target3, 3);
		}
	}

	void HeroAllStatusTexture(GameObject target , int num)
	{
		float hpCurrent = (float)Patterns.getHealthPower (target);
		float hpMax = (float)Patterns.getHealthPowerMax (target);
		float hpBarValue = hpCurrent/hpMax;
		float militaryStrengthCurrent = (float)Patterns.AlliesFriendList(target , 0).Count;
		float leaderShip = (float)Patterns.getLeaderShip (target);
		float militaryStrengthValue = militaryStrengthCurrent / leaderShip;
		if (num == 1) 
		{
			UI_Hero_All_Icon_One.texture = (Texture)Resources.Load (getIconPath(target));
			UI_Hero_All_Hp_One.value = hpBarValue;
			UI_Hero_All_MilitaryStrength_One.value = militaryStrengthValue;
		}
		if (num == 2) 
		{
			UI_Hero_All_Icon_Two.texture = (Texture)Resources.Load (getIconPath(target));
			UI_Hero_All_Hp_Two.value = hpBarValue;
			UI_Hero_All_MilitaryStrength_Two.value = militaryStrengthValue;
		}
		if (num == 3) 
		{
			UI_Hero_All_Icon_Three.texture = (Texture)Resources.Load (getIconPath(target));
			UI_Hero_All_Hp_Three.value = hpBarValue;
			UI_Hero_All_MilitaryStrength_Three.value = militaryStrengthValue;
		}

	}

	void HeroOneStatusUI(GameObject target)
	{
		if (target != null)
		{
			float hpCurrent = (float)Patterns.getHealthPower (target);
			float hpMax = (float)Patterns.getHealthPowerMax (target);
			float hpBarValue = hpCurrent/hpMax;
			UI_Hero_One_HP.value = hpBarValue;
			if (hpCurrent < hpMax * 0.25) {
				UI_Hero_One_HP_Bar.color = new Color (255f, 0f, 0f);
			} else {
				UI_Hero_One_HP_Bar.color = new Color (0f, 255f, 0f);
			}
			UI_Hero_One_AttIcon_Value.text = Patterns.getAttack (target).ToString ();
			UI_Hero_One_DefIcon_Value.text = Patterns.getDefence (target).ToString ();
			UI_Hero_One_DexIcon_Value.text = Patterns.getDexterity (target).ToString ();
			UI_Hero_One_AgiIcon_Value.text = Patterns.getAgility (target).ToString ();
			HeroOneStatusTexture (target);
		}
	}
		
	void HeroOneStatusInitialize()
	{
		UI_Hero_One_Skill_Active1.GetComponent<Button> ().onClick.RemoveAllListeners ();
		UI_Hero_One_Skill_Active2.GetComponent<Button> ().onClick.RemoveAllListeners ();
		UI_Hero_One_Skill_Active3.GetComponent<Button> ().onClick.RemoveAllListeners ();
	}

	void HeroOneStatusTexture(GameObject target)
	{
		if(UI_Target != target)
		{
			//status
			UI_Hero_One_AttIcon.texture = (Texture)Resources.Load ("Icon/Icon_Att");
			UI_Hero_One_DefIcon.texture = (Texture)Resources.Load ("Icon/Icon_Def");
			UI_Hero_One_DexIcon.texture = (Texture)Resources.Load ("Icon/Icon_Dex");
			UI_Hero_One_AgiIcon.texture = (Texture)Resources.Load ("Icon/Icon_Agi");

			//skill

			HeroOneStatusInitialize ();
			ArrayList activeSkill = Patterns.getActiveSkillSelect (target);
			UI_Hero_One_Skill_Active1.GetComponent<Button>().onClick.AddListener(delegate {
				OnBUttonClickForSkill(target , activeSkill[0].ToString());
			});
			UI_Hero_One_Skill_Active1_Texture.texture = (Texture)Resources.Load ("Icon/Icon_" + activeSkill[0]);
			UI_Hero_One_Skill_Active2.GetComponent<Button> ().onClick.AddListener (delegate {
				OnBUttonClickForSkill (target , activeSkill[1].ToString());
			});
			UI_Hero_One_Skill_Active2_Texture.texture = (Texture)Resources.Load ("Icon/Icon_" + activeSkill[1]);
			UI_Hero_One_Skill_Active3.GetComponent<Button> ().onClick.AddListener (delegate {
				OnBUttonClickForSkill (target , activeSkill[2].ToString());
			});
			UI_Hero_One_Skill_Active3_Texture.texture = (Texture)Resources.Load ("Icon/Icon_" + activeSkill[2]);
			ArrayList passiveSkillTexture = Patterns.getPassiveSkillSelect (target);
			UI_Hero_One_Skill_Passive1_Texture.texture = (Texture)Resources.Load ("Icon/Icon_" + passiveSkillTexture[0]);
			UI_Hero_One_Skill_Passive2_Texture.texture = (Texture)Resources.Load ("Icon/Icon_" + passiveSkillTexture[1]);
			UI_Target = target;
		}
	}

	void OnBUttonClickForSkill(GameObject target , string skillName)
	{
		Skills.SkillSpell (target, skillName);
		return;
	}

	string getIconPath(GameObject target)
	{
		string iconPath = ("Icon/Icon_" + target.gameObject.name).Replace ("(Clone)", "");
		return iconPath;
	}
}
