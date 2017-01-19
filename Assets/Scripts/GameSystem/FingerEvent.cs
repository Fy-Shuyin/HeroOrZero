﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class FingerEvent{

	public void ClickPoint(GameObject hero)
	{
		var putDown = Input.GetKey (KeyCode.Mouse0);
		int touch = Input.touchCount;
		if (putDown) {
			if (EventSystem.current.IsPointerOverGameObject ()) {
				return;
			}
			Ray clickRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (clickRay, out hit)) 
			{
				if (hit.collider.tag.Equals ("Enemy")) {
					hero.GetComponent<HeroController> ().Target (hit.transform);
				}
				else 
				{
					if(hit.collider.name.Equals ("GroundOfBattle"))
						hero.GetComponent<HeroController> ().ChangeToMoveStage (hit.point);
				}
			}
		}

		if (touch == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
				return;
			}
			Ray touchRay = Camera.main.ScreenPointToRay (Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast (touchRay, out hit)) 
			{
				if (hit.collider.tag.Equals ("Enemy")) {
					hero.GetComponent<HeroController> ().Target (hit.transform);
				}
				else 
				{
					if(hit.collider.name.Equals ("GroundOfBattle"))
						hero.GetComponent<HeroController> ().ChangeToMoveStage (hit.point);
				}
			}
		}
	}

	public string ClickName()
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

	public string ClickTag()
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
}
