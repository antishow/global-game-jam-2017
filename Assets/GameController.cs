using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	private static GameController _instance;

	void Awake(){
		_instance = this;
	}

	public static void GameOver(){
		print("GAME OVER");
		CameraEffectsController.IrisOut();
		_instance.Invoke("OnIrisOut", CameraEffectsController.IrisTime * 1.1f);
	}

	public void OnIrisOut(){
		print("Game Over. Play again?");
	}
}
