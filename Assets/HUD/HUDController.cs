﻿using System.Collections;
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
		_instance.ConfidenceMeter.transform.rotation = Quaternion.Euler(0, 0, theta);
		Conductor.MixForConfidence(confidence);
	}

	public static void UpdatePlayerExpression(int expression){
		_instance.PlayerPortrait.sprite = _instance.PlayerPortraits[expression];
	}
}