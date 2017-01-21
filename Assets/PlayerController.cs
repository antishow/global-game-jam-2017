using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public const int UNHAPPY = 0;
	public const int NEUTRAL = 1;
	public const int HAPPY = 2;

	private float _confidence = 1.0f;
	protected float Confidence{
		get { return _confidence; }
		set { 
			_confidence = Mathf.Clamp(value, 0.0f, 1.0f); 
			Expression = Mathf.FloorToInt(_confidence * 2.9f);
			HUDController.UpdatePlayerConfidence(_confidence);
		}
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
		Confidence = 1.0f;
		InvokeRepeating("NudgeConfidence", 0, 0.0166f);
	}

	private bool nudgeUpwards = true;

	void NudgeConfidence () {
		float nudge = 0.0005f;
		if(!nudgeUpwards){
			nudge *= -1;
		}

		Confidence += nudge;
		if(Confidence == 1 || Confidence == 0){
			nudgeUpwards = !nudgeUpwards;
		}
	}
}
