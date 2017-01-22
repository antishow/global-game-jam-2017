using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitmentMeterScript : MonoBehaviour {

	public PlayerMove PlayerMoveScript;
	public MeterScript ArmMeter;
	public MeterScript GazeMeter;
	public MeterScript WaveMeter;
	public Color GoodColor;
	public Color BadColor;

	// Use this for initialization
	void Start () {
		ArmMeter.SetColor(GoodColor);
		GazeMeter.SetColor(GoodColor);
		WaveMeter.SetColor(GoodColor);

	}
	
	// Update is called once per frame
	void Update () {
	ArmMeter.SetValue(PlayerMoveScript.ArmMagnitude);
	GazeMeter.SetValue(PlayerMoveScript.GazeMagnitude);
	GazeMeter.SetValue(PlayerMoveScript.WaveMagnitude);
	}
	public float GetCommitment (bool Success)
	{
		float RETURN = PlayerMoveScript.ArmMagnitude+PlayerMoveScript.GazeMagnitude +PlayerMoveScript.WaveMagnitude;

		return RETURN;
	}
	
}
