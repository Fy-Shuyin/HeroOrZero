using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.EventSystems;

public class FingerEvent{

	public void ClickPoint(GameObject hero)
	{
		var putDown = Input.GetKey (KeyCode.Mouse0);
		if (putDown) {
			if (IsPointerOverGameObject (Input.mousePosition)) {
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
			return;
		}

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			if (IsPointerOverGameObject (Input.GetTouch(0).position)) 
			{
				return;
			}
			else 
			{
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
	}

	bool IsPointerOverGameObject(Vector2 touchPosition)
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(touchPosition.x, touchPosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
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
