﻿using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			LoadStage.Globe.loadName = 1;
			Application.LoadLevel(1);
		}
	}
}
