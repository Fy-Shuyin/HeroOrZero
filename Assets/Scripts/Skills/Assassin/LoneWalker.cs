using UnityEngine;
using System.Collections;

public class LoneWalker : MonoBehaviour {

	PatternsOfAttribute Attribute = new PatternsOfAttribute();

	private bool isAlone;
	private int Level;
	public string SkillType;
	public int SkillMethod;
	private float Range;
	private float Value;
	private GameObject leftHand;
	private GameObject rightHand;

	void Start () 
	{
		skillInitialize ();
	}

	void Update () 
	{
		int allyNum = Attribute.AlliesFriendList (gameObject,Range).Count;
		if (allyNum == 0 && !isAlone) 
		{
			addAttribute (gameObject, Value);
			Transform[] transform = GetComponentsInChildren<Transform> (true);
			foreach (Transform child in transform)
			{
				if (child.name == "B_L_Hand") 
				{
					var prefab1 = Resources.Load ("Skills/LoneWalker");
					leftHand = Instantiate (prefab1) as GameObject;
					leftHand.transform.SetParent (child);
					leftHand.transform.localScale = new Vector3 (1, 1, 1);
					leftHand.transform.localPosition = new Vector3 (0, 0, 0);
				}
				if (child.name == "B_R_Hand") 
				{
					var prefab2 = Resources.Load ("Skills/LoneWalker");
					rightHand = Instantiate (prefab2) as GameObject;
					rightHand.transform.SetParent (child);
					rightHand.transform.localScale = new Vector3 (1, 1, 1);
					rightHand.transform.localPosition = new Vector3 (0, 0, 0);
				}
			}
			isAlone = true;
		} 
		if (allyNum > 1 && isAlone || !Attribute.TargetIsLive(gameObject)) 
		{
			lessAttribute (gameObject, Value);
			Destroy(leftHand);
			Destroy(rightHand);
			isAlone = false;
		}
	}

	void skillInitialize()
	{
		Level = 1;
		SkillType = "Permanent";
		SkillMethod = 7;			
		Value = 15;
		Range = 20;
	}

	public void addAttribute(GameObject target, float value)
	{
		Attribute.setAttack (target, value);
		Attribute.setDefence (target, value);
		Attribute.setDexterity (target, value);
		Attribute.setAgility (target, value);
	}

	public void lessAttribute(GameObject target, float value)
	{
		value *= -1;
		Attribute.setAttack (target, value);
		Attribute.setDefence (target, value);
		Attribute.setDexterity (target, value);
		Attribute.setAgility (target, value);
	}
}
