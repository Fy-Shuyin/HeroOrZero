using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;

public class SceneBattle : MonoBehaviour {

	GameSystem gamesystem;
	PatternsOfAttribute Patterns = new PatternsOfAttribute();
	PatternsOfSkills Skills = new PatternsOfSkills();

	public GameObject Battle_Camera;
	bool Battle_CameraOn;
	public GameObject Battle_Camera_Target;
	public GameObject UI_Target;

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
		HeroLoad ();
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
		isHeroLive ();
		HeroAllStatusUI (Hero_One, Hero_One, Hero_One);
		HeroOneStatusUI (Battle_Camera_Target);
	}

	void CameraOn()
	{
		if (Battle_CameraOn)
		{
			Battle_Camera.transform.position = new Vector3(Battle_Camera_Target.transform.position.x,25f,Battle_Camera_Target.transform.position.z-15);
		} else 
		{
			Battle_Camera.transform.position = new Vector3 (0,80,-70);
		}
	}

	void HeroLoad()
	{
		var prefab_One = Resources.Load ("Heros/Swordman");
		Hero_One_Icon = GameObject.Find ("Hero_One_Icon");
		if (prefab_One != null) 
		{
			Hero_One = Instantiate (prefab_One) as GameObject;
			Hero_One.AddComponent<HeroController> ();
			Hero_One.transform.position = point [1];
			Hero_One_Icon.GetComponent<Button> ().onClick.AddListener (delegate() {
				OnButtonClick (Hero_One);
			});
			string iconPath = ("Icon/Icon_" + Hero_One.gameObject.name).Replace ("(Clone)", ""); 
			Hero_One_Icon.GetComponentInChildren<RawImage> ().texture = (Texture)Resources.Load (iconPath);
		}
		else 
		{
			Hero_One_Icon.SetActive (false);
		}
	}

	void isHeroLive()
	{
		if (!Hero_One.GetComponent<HeroController> ().IsLife || Hero_One == null) 
		{
			Hero_One_Icon.GetComponent<Button> ().onClick.RemoveAllListeners ();
			if (Battle_CameraOn) 
			{
				Battle_CameraOn = false;
				setBattleCameraTargetandGUITarget (null);
			}
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

	void OnButtonClick(GameObject hero)
	{ 
		if (Battle_CameraOn) 
		{
			Battle_CameraOn = false;
			hero.GetComponent<HeroController> ().setAIMode (true);
			setBattleCameraTargetandGUITarget (null);
		} else if (!Battle_CameraOn) 
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
		float militaryStrengthCurrent = (float)Patterns.AlliesFriend(target , 0).Count;
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
