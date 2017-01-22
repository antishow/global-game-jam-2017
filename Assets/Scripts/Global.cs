using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Options{
	public bool headbobActive;
	public bool showTutorial;
}

public class Global : MonoBehaviour {
	public static Options options;
	public GameObject headbobToggleMenuOption;
	public GameObject showTutorialMenuOption;
	public static bool pause;
	// Use this for initialization
	void Start () {
		JsonFileReader.checkLoading();
		if(JsonFileReader.checkFileWithContent("/Options/Options.json", "{ \"headbobActive\":true,\"showTutorial\":true }")){
			options = JsonUtility.FromJson<Options>(JsonFileReader.LoadJsonStringAsExternal("/Options/Options.json"));
		}
		headbobToggleMenuOption.GetComponent<Toggle>().isOn = options.headbobActive;
		showTutorialMenuOption.GetComponent<Toggle>().isOn = options.showTutorial;
	}
	
	// Update is called once per frame
	void Update () {
		if(headbobToggleMenuOption.GetComponent<Toggle>().isOn != options.headbobActive){
			options.headbobActive = headbobToggleMenuOption.GetComponent<Toggle>().isOn;
			JsonFileReader.WriteJsonToFile(Application.persistentDataPath + "/Options/Options.json", JsonUtility.ToJson(options));
		}
		if(showTutorialMenuOption.GetComponent<Toggle>().isOn != options.showTutorial){
			options.showTutorial = showTutorialMenuOption.GetComponent<Toggle>().isOn;
			JsonFileReader.WriteJsonToFile(Application.persistentDataPath + "/Options/Options.json", JsonUtility.ToJson(options));
		}

		if(Global.pause){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}
}
