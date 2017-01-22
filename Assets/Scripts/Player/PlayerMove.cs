using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;


[System.Serializable]
public class PlayerSoundEffects{
	public AudioClip[] WaveSuccess;
	public AudioClip[] WaveFail;
	public AudioClip SlideUp;
	public AudioClip SlideDown;
	public AudioClip Wave;
}

public class PlayerMove : MonoBehaviour {
	public PlayerSoundEffects SFX;
	private AudioSource audioSource;
	private float SlideWhistleDuration = 2.0f;

	public Animator HandWaveAnim; 
	public Vector3 PlayerPosition;
	public float MoveSpeed;
	public Vector2 WalkMotionSpeed;
	//public float WalkMotionMagnitude;
	public Vector2 WalkMotionfactor; 
	Vector3 WalkMotionOffset = new Vector3(0, 0, 0);
 
	public Transform ArmMove; 
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

	public Vector2 EyeGaze;
	public float GazeMagnitude;

	private bool IsArmMoving = false;
	private bool IsArmMovingUp = false;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetPosition();
		SetWalkMotion();
		GetInput();
		if (GetGaze())
		{
			ChechGazeCollide();
		}
        SetArmHeight();
		ShowHandWave();

	}

	private void ChechGazeCollide ()
	{
		Ray rayToPlayerPos = Camera.main.ScreenPointToRay(EyeGaze);
		RaycastHit hitInfo;
		if (Physics.Raycast(rayToPlayerPos, out hitInfo, 1000))
		{
			
			//assuming only npcs have colliders any hit could be a stare

		}

    }

	private void ShowHandWave ()
	{
		HandWaveAnim.SetFloat("waveMagnitude", WaveMagnitude);
    }

	private void SetArmHeight ()
	{
		ArmMove.transform.localPosition = Vector3.Lerp(ArmMove.transform.localPosition, new Vector3(0, ArmHeight, 0),.1f);
		//float ArmRotation = (ArmMaxHeight - ArmHeight) * ArmMinRotation;
		//ArmRotate.transform.rotation = Quaternion.Euler(ArmRotation, 0, 0);
	}

	private void GetInput ()
	{
		float dY = 0;

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
				dY = Time.deltaTime * ArmRaiseSpeed;
				Down = DownWait;
			}				

		}
		else
		{
			if (Down<=0)
			{ 
				dY = -1 * Mathf.Clamp(Time.deltaTime * ArmRaiseSpeed, 0, ArmMaxHeight);
			}			
		}
		if (Wave > 0)
		{
			Wave -= Time.deltaTime;
		}
		else
		{
			WaveMagnitude -=Time.deltaTime;
		}
		if (Down>0)
		{
			Down -= Time.deltaTime;
		}

		float newMagnitude = Mathf.Clamp01(ArmMagnitude + dY);
		dY = newMagnitude - ArmMagnitude;

		if(Mathf.Abs(dY) > 0){
			if(!IsArmMoving){
				if(dY > 0){
					StartMovingArmUp();
				} else {
					StartMovingArmDown();
				}
			} else if(IsArmMoving){ 
				if(!IsArmMovingUp && dY > 0){
					StartMovingArmUp();
				} else if(IsArmMovingUp && dY < 0){
					StartMovingArmDown();
				}
			}

			IsArmMoving = true;
		} else {
			if(IsArmMoving){
				StopMovingArm();
			}
			IsArmMoving = false;
		}

		ArmMagnitude = newMagnitude;
		ArmHeight = ((ArmMaxHeight - ArmMinHeight) * ArmMagnitude) + ArmMinHeight;
	}

	private void StartMovingArmUp(){
		IsArmMovingUp = true;
		IsArmMoving = true;
		if(audioSource.isPlaying){
			audioSource.Stop();
		}
		float iLerp = Mathf.InverseLerp(ArmMinHeight, ArmMaxHeight, ArmHeight);
		audioSource.clip = SFX.SlideUp;
		audioSource.time = iLerp * SlideWhistleDuration;
		audioSource.Play();
	}

	private void StartMovingArmDown(){
		IsArmMovingUp = false;
		IsArmMoving = true;
		if(audioSource.isPlaying){
			audioSource.Stop();
		}
		float iLerp = Mathf.InverseLerp(ArmMinHeight, ArmMaxHeight, ArmHeight);
		audioSource.clip = SFX.SlideDown;
		audioSource.time = iLerp * SlideWhistleDuration;
		audioSource.Play();
	}

	private void StopMovingArm(){
		IsArmMoving = false;
		if(audioSource.isPlaying){
			audioSource.Stop();
		}
	}

	private void WaveArm (float w)
	{
		if(audioSource.isPlaying){
			audioSource.Stop();
		}
		audioSource.clip = SFX.Wave;
		audioSource.Play();
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
	private bool GetGaze ()//may need to validate getgaze before using 
	{
		 EyeGaze = EyeTracking.GetGazePoint().Screen;
		if (EyeGaze.x>0&&EyeGaze.y>0&&EyeGaze.x<Screen.width&&EyeGaze.y<Screen.height)
		{
			return true;
		}
		return false;

	}
}
