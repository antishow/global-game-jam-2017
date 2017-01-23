using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public const int UNHAPPY = 0;
	public const int NEUTRAL = 1;
	public const int HAPPY = 2;

	public float TimeToLiveWithNoConfidence = 5.0f;
	public bool Dying = false;

	private float _confidence = 1.0f;
	protected float Confidence{
		get { return _confidence; }
		set { 
			_confidence = Mathf.Clamp(value, 0.0f, 1.0f); 
			Expression = Mathf.FloorToInt(_confidence * 2.9f);
			HUDController.UpdatePlayerConfidence(_confidence);
			CameraEffectsController.ApplyConfidenceToEffects(_confidence);

			if(Confidence <= 0){
				StartDying();
			} else if(Dying){
				CancelDying();
			}
		}
	}

	public float GetConfidence(){
		return Confidence;
	}
	public void SetConfidence(float c){
		Confidence = c;
	}

	public void NudgeConfidence(bool increment, float value){
		if(!increment){
			value *= -1;
		}

		Confidence += value;
	}

	private void StartDying(){
		Dying = true;
		Invoke("Die", TimeToLiveWithNoConfidence);
		CancelInvoke("NudgeConfidence");
	}

	private void CancelDying(){
		CancelInvoke("Die");
	}

	private void Die(){
		Global.GameOver();
	}

	private int _expression;
	protected int Expression{
		get { return _expression; }
		set { 
			_expression = value;
			HUDController.UpdatePlayerExpression(_expression);
		}
	}
	
	void Start () {
		Confidence = 1f;
		Dying = false;
	}
}
