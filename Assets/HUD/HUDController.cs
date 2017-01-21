using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	private static HUDController _instance;

	private GameObject ConfidenceMeter;
	private Image PlayerPortrait;

	public static float ConfidenceMeterRotationExtent = 45.0f;

	public Sprite[] PlayerPortraits;

	void Awake(){
		_instance = this;

		ConfidenceMeter = transform.Find("Confidence Meter").gameObject;
		PlayerPortrait = transform.Find("Player Portrait").gameObject.GetComponent<Image>();
	}

	public static void UpdatePlayerConfidence(float confidence){
		float theta = Mathf.Lerp(ConfidenceMeterRotationExtent, -ConfidenceMeterRotationExtent, confidence);
		Debug.LogFormat("Set Confidence Level to {0} ({1})", confidence, theta);
		_instance.ConfidenceMeter.transform.rotation = Quaternion.Euler(0, 0, theta);
	}

	public static void UpdatePlayerExpression(int expression){
		Debug.LogFormat("Set sprite to {0}", expression);
		_instance.PlayerPortrait.sprite = _instance.PlayerPortraits[expression];
	}
}
