using UnityEngine;
using System.Collections;

public class ObjectEffect : MonoBehaviour {

	bool ratate;
	float speed;
	string axis;

	void Start () {
	
	}
	
	void Update () 
	{
		if (ratate) 
		{
			if (axis == "y") {
				gameObject.transform.Rotate (gameObject.transform.up * speed);
			}
		}
	}

	public void setAttribute(bool ratate , string axis , float speed)
	{
		this.ratate = ratate;
		this.axis = axis;
		this.speed = speed;
	}
}
