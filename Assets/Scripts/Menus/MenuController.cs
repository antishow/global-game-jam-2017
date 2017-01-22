using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	public GameObject menuObject;
	public string buttonString;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(buttonString)){
			menuObject.active = !menuObject.active;
			Global.pause = menuObject.active;
		}
	}	

	public void CloseMenu () {
		menuObject.active = false;
		Global.pause = false;
	}

	public void ExitGame () {
		Application.Quit();
	}
}
