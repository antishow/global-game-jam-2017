using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;

public class JsonFileReader : MonoBehaviour {
	public static bool modsOK;
	public static string LoadJsonStringAsResource(string path){
		string filePath = path.Replace(".json", "");
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		return targetFile.text;
	}

	public static string LoadJsonStringAsExternal(string path){
		StreamReader reader = new StreamReader(Application.persistentDataPath + path);
		string response = "";
		while(!reader.EndOfStream)
		{
			response += reader.ReadLine();
		}
		reader.Close();
		return response;
	}

	public static void WriteJsonToFile(string path, string content)
	{
		if (!System.IO.File.Exists(path)){
			Debug.Log("Writing to: " + path);
			using (FileStream fs = File.Create(path)){
				Byte[] info = new UTF8Encoding(true).GetBytes(content);
				// Add some information to the file.
				fs.Write(info, 0, info.Length);
			}
		} else {
			using (FileStream fs = File.Create(path)){
				Byte[] info = new UTF8Encoding(true).GetBytes(content);
				// Add some information to the file.
				fs.Write(info, 0, info.Length);
			}
		}
	}

	public static bool checkFile(string AppPath){
		string path = Application.persistentDataPath + AppPath;
		if (!System.IO.File.Exists(path)){
			JsonFileReader.WriteJsonToFile(path, "{}");
			return true;
		} else {
			return true;
		}
	}

	public static bool checkFolder(string AppPath){
		string path = Application.persistentDataPath + AppPath;
		if (!System.IO.Directory.Exists(path)){
			System.IO.Directory.CreateDirectory(@path);
			return true;
		} else {
			return true;
		}
	}

	public static bool checkFileWithContent(string AppPath, string defaultContent){
		string path = Application.persistentDataPath + AppPath;
		Debug.Log(path);
		if (!System.IO.File.Exists(path)){
			Debug.Log("Writing");
			JsonFileReader.WriteJsonToFile(path, defaultContent);
			return true;
		} else {
			return true;
		}
	}

	public static bool checkLoading(){
		//if we've checked the config files and such we can skip checking again
		if(modsOK){
			return true;
		}
		//Check all base mod folders to make sure the config files exist
		if(
			JsonFileReader.checkFolder("")
			&& JsonFileReader.checkFolder("/Options")
		){
			modsOK = true;
			return true;
		} else {
			return false;
		}
	}
}