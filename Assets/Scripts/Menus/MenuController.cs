using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	public GameObject pauseMenu;

	public string pauseButton;
	private GameObject GameOverMenu;

	void Awake(){
		GameOverMenu = transform.Find("EventSystem/GameOverMenu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(pauseButton)){
			pauseMenu.active = !pauseMenu.active;
			Global.pause = pauseMenu.active;
		}
	}	

	public void DisplayGameOverScreen(){
		GameOverMenu.SetActive(true);
	}

	public void CloseMenu () {
		pauseMenu.active = false;
		Global.pause = false;
	}

	public void ExitGame () {
		Application.Quit();
	}

	public void TryAgain(){
		SceneManager.LoadScene("StreetScene");
	}
}
