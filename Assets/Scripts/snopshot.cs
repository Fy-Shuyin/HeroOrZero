using UnityEngine;
using System.Collections;

public class snopshot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var putDown = Input.GetKey (KeyCode.M);
		if (putDown) {
			Application.CaptureScreenshot ("Assets/name.png");
			Debug.Log ("picture");
		}
	}
}
