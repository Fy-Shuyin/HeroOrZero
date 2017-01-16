using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class FingerEvent{

	public void MouthClickForPoint(GameObject hero)
	{
		var putDown = Input.GetKey (KeyCode.Mouse0);
		if (putDown) 
		{
			if (EventSystem.current.IsPointerOverGameObject()) {
				return;
			}
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag.Equals ("Enemy")) {
					hero.GetComponent<HeroController> ().Target (hit.transform);
				}
				else 
				{
					hero.GetComponent<HeroController> ().ChangeToMoveStage (hit.point);
				}
			}
		}
	}

	public string MouthClickForName()
	{
		string putName = "";
		var putDown = Input.GetKeyDown (KeyCode.Mouse0);
		if (putDown) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				putName = hit.transform.name;
			}
		}
		Debug.Log (putName);
		return putName;
	}

	public string MouthClickForTag()
	{
		var putDown = Input.GetKeyDown (KeyCode.Mouse0);
		string targetTag = "";
		if (putDown) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				targetTag = hit.transform.tag;
			}
		}
		Debug.Log (targetTag);
		return targetTag;
	}

	public bool SelectUnit(Vector3 point , bool selected)
	{
		float time = 300f;
		point = new Vector3();
		selected = false;

		Time.timeScale = 0;
		if (MouthClickForTag () == "Hero" || MouthClickForTag () == "Friend" || MouthClickForTag () == "Enemy") {

			selected = true;
			Time.timeScale = 1;

			Debug.Log (MouthClickForTag());
		} 
		else 
		{
			time--;
			if (time <= 0) 
			{
				Time.timeScale = 1;
			}
		}
		Debug.Log (point);
		Debug.Log (selected);
		Debug.Log (time);
		return selected;
	}

	public void SelectGroup()
	{
	}

	public void MouthClick(Button button)
	{
		
	}

	void TouchCount () 
	{
		if (Input.touchCount > 0) 
		{
			
		}
	}
}
