using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneUIManeger : MonoBehaviour {

	FingerEvent fingerEvent = new FingerEvent();

	public GameObject panelHeroStatus, panelTeamStatus;
	public Button[] closePanel;
	public Button[] skillSelect;
	public Button[] skillActive;
	public Button[] skillPassive;
	public Button[] teamSelect;
	string touchName = null;

	void Start () {
		//panelTeamStatus.SetActive (false);
		//panelHeroStatus.SetActive (false);
		foreach (Button button in closePanel) 
		{
			Debug.Log (button.name);
			Debug.Log(getButtonPanel(button).name);
			button.onClick.AddListener (delegate() {
				getButtonPanel(button).SetActive(false);
			});
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fingerEvent.ClickName () == "TownHall") 
		{
		}

		if (fingerEvent.ClickName () == "Tavern") 
		{
			Debug.Log (fingerEvent.ClickName ());
			panelHeroStatus.SetActive(true);
		}

		if (fingerEvent.ClickName () == "Farm") 
		{
			Debug.Log (fingerEvent.ClickName ());
		}

		if (fingerEvent.ClickName () == "Team Select (1)" || fingerEvent.ClickName () == "Team Select (2)") 
		{
			panelTeamStatus.SetActive (true);
		}
	}

	void OnButtonClick(string buttonName)
	{
		buttonName = fingerEvent.ClickName ();
		Debug.Log (buttonName);
		for (int i = 0; i < closePanel.Length; i++) 
		{
			if (buttonName == closePanel [i].name) 
			{
				Debug.Log (closePanel [i].name);
				GameObject obj = getButtonPanel (closePanel [i]);
				obj.SetActive (false);
			}
		}
	}

	GameObject getButtonPanel(Button button)
	{
		return button.transform.parent.gameObject;
	}
}
