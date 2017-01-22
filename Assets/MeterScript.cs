﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterScript : MonoBehaviour {
	[Range(0.0f, 1.0f)]
	public float Value;
	public Transform Middle;
	public Transform Top;
	float Top_min = -1;
	float Top_max = 1.11f;
	float Middle_min = -1;
	float	Middle_max=0;
	float Middle_max_scale = 7;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Top.localPosition = new Vector3(0, (Top_max - Top_min) * Value + Top_min, -.1f);
		Middle.localPosition = new Vector3(0, (Middle_max - Middle_min) * Value + Middle_min, -.1f);
		Middle.localScale = new Vector3(1, Middle_max_scale * Value, 1);
	}
}