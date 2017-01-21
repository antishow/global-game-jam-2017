using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	
	public Vector3 PlayerPosition;
	public float MoveSpeed;
	public Vector2 WalkMotionSpeed;
	//public float WalkMotionMagnitude;
	public Vector2 WalkMotionfactor; 
	Vector3 WalkMotionOffset = new Vector3(0, 0, 0);
 
	public Transform ArmMove;
        public Transform ArmRotate;
	//public float ArmPosition;
	public float ArmMaxHeight;
	public float ArmMinHeight;
	public float ArmMinRotation;
	public float ArmRaiseSpeed;

	public float ArmMagnitude;
	public float ArmHeight;

	public float DownWait ;
	public float WaveWait ;
	public float Down = 0;
	public float Wave = 0;
	 
	public float WaveMagnitude = 0; 



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetPosition();
		SetWalkMotion();
		GetInput();
		SetArmHeight();
		 
	}

	

	private void SetArmHeight ()
	{
		ArmMove.transform.localPosition = Vector3.Lerp(ArmMove.transform.localPosition, new Vector3(0, ArmHeight, 0),.1f);
		float ArmRotation = (ArmMaxHeight - ArmHeight) * ArmMinRotation;
		ArmRotate.transform.rotation = Quaternion.Euler(ArmRotation, 0, 0);
	}

	private void GetInput ()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Wave = WaveWait;
			Down = DownWait;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Wave > 0)
			{			 
			 	WaveArm(WaveWait - Wave);
			}	
		}

		if (Input.GetKey(KeyCode.Space))
		{
			if (Wave<=0)
			{
				ArmMagnitude += Time.deltaTime * ArmRaiseSpeed;
				Down = DownWait;
			}				

		}
		else
		{
			if (Down<=0)
			{ 
				ArmMagnitude -= Mathf.Clamp(Time.deltaTime * ArmRaiseSpeed, 0, ArmMaxHeight);
			}			
		}
		if (Wave > 0)
		{
			Wave -= Time.deltaTime;
		}
		else
		{
			WaveMagnitude = 0;
		}
		if (Down>0)
		{
			Down -= Time.deltaTime;
		}
		ArmMagnitude = Mathf.Clamp01(ArmMagnitude);
		ArmHeight = ((ArmMaxHeight - ArmMinHeight) * ArmMagnitude) + ArmMinHeight;
	}

	private void WaveArm (float w)
	{
		WaveMagnitude += w*ArmMagnitude*.4f;//******** Add factor based on current rotation
		WaveMagnitude = Mathf.Clamp01(WaveMagnitude);
	}
	

	private void SetWalkMotion ()
	{
		float zLoc = transform.position.z  ;
		WalkMotionOffset.x = (Mathf.Sin(zLoc*WalkMotionSpeed.x) *   WalkMotionfactor.x);
		WalkMotionOffset.y = (Mathf.Sin(zLoc* WalkMotionSpeed.y) *   WalkMotionfactor.y);
		//PlayerPosition.z = (Mathf.Sin(zLoc) *   WalkMotionfactor.z);
		//float WalkRotation = (Mathf.Sin(zLoc) * WalkMotionMagnitude) - (WalkMotionMagnitude / 2);
		//transform.rotation = Quaternion.Euler(0, 0, WalkRotation);
	}

	private void SetPosition ()
	{
		PlayerPosition.z += MoveSpeed * Time.deltaTime;
		
		this.transform.position = PlayerPosition+WalkMotionOffset;
	}
}
