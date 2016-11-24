using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {

	float AlphaTime;
	float time;

	void Update () 
	{
		if (AlphaTime <= 0) 
			time = 1;
		if (AlphaTime >= 5)
			time = -1;
		AlphaTime -= time * Time.deltaTime;

	}
		
}
