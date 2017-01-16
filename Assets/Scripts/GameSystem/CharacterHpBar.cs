using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterHpBar : MonoBehaviour {

	PatternsOfAttribute Patterns = new PatternsOfAttribute ();

	public Slider HpBar;
	public Image HpColor;

	private GameObject targetCharacter;
	private float hightCharacter;
	private Canvas canvas;
	private RectTransform rectTransform;
	private RectTransform parentRectTransform;
	private Vector2 localPos;

	void Start () 
	{
		//Canvas
		var canvasObject = GameObject.Find("Canvas");
		canvas = canvasObject.GetComponent<Canvas> ();
		rectTransform = GetComponent<RectTransform> ();
		parentRectTransform = canvasObject.GetComponent<RectTransform> ();

		//Parent
		transform.SetParent(canvasObject.transform);

		//Bar
		HpBar.value = HpBar.maxValue;

		//Hight
		//hightCharacter = targetCharacter.transform.localScale.y * targetCharacter.GetComponent<Collider> ().bounds.size.y;
		hightCharacter = targetCharacter.GetComponent<Collider> ().bounds.size.y;
		//Debug.Log (hightCharacter);
	}

	void Update ()
	{
		//Delete
		if (targetCharacter == null) 
		{
			Destroy (gameObject);
			return;
		}

		//Position
		var screenPos = Camera.main.WorldToScreenPoint(targetCharacter.transform.position + new Vector3(0f , hightCharacter , 0f));
		RectTransformUtility.ScreenPointToLocalPointInRectangle (parentRectTransform, screenPos, canvas.worldCamera, out localPos);
		rectTransform.localPosition = localPos;

		//Value
		float hpCurrent = (float)Patterns.getHealthPower (targetCharacter);
		float hpMax = (float)Patterns.getHealthPowerMax (targetCharacter);
		float hpBarValue = hpCurrent/hpMax;
		HpBar.value = hpBarValue;
		if (hpCurrent < hpMax * 0.25) {
			HpColor.color = new Color (255f, 0f, 0f);
		} else {
			HpColor.color = new Color (0f, 255f, 0f);
		}
	}

	public void Initialize(GameObject target)
	{
		targetCharacter = target;
	}
}
