using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FingerEvent {

	public bool MouthClick()
	{
		var putDown = Input.GetKeyDown (KeyCode.Mouse0);
		return putDown;
	}

	public Vector3 MouthClickForPoint(Vector3 putPoint)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit, 100, 5)) 
		{
			putPoint = hit.point;
			Debug.Log (putPoint);
		}
		return putPoint;
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
