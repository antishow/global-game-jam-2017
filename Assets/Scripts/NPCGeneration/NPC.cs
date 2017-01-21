using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCItems{
	public NPCHair hairID;
	public NPCEars earsID;
	public NPCNose noseID;
	public NPCShirt shirtID;
	public NPCPants pantsID;

	public Color hairColor;
	public Color skinColor;
	public Color shirtColor;
	public Color pantsColor;
}

public class NPC : MonoBehaviour {
	public NPCItems npcItems;
	public float npcSpeed = 5;
	public float npcSlowSpeed = 3;
	public float eyeContactThreshold = 15;
	public float waveDistanceThreshold = 8;
	public float chanceToWave = 50.0f;
	public float chanceToWaveAtPlayer = 50.0f;

	public bool goingToWave = false;
	public bool goingToWaveAtPlayer = false;
	public bool isWaving = false;
	public bool isWavingAtPlayer = false;
	public GameObject[] hairPrefabs;
	public GameObject[] earPrefabs;
	public GameObject[] nosePrefabs;
	public GameObject[] shirtPrefabs;
	public GameObject[] pantPrefabs;
	public static GameObject playerObject;
	// Use this for initialization
	void Start () {
		if(playerObject == null){
			playerObject = GameObject.FindWithTag("Player");
		}
		if(Random.Range(0.0f, 100.0f) >= chanceToWave){
			goingToWave = true;
		}
		if(goingToWave && Random.Range(0.0f, 100.0f) >= chanceToWaveAtPlayer){
			goingToWaveAtPlayer = true;
		}

		npcItems.hairID = (NPCHair) Random.Range(0,hairPrefabs.Length);
		npcItems.earsID = (NPCEars) Random.Range(0,earPrefabs.Length);
		npcItems.noseID = (NPCNose) Random.Range(0,nosePrefabs.Length);
		npcItems.shirtID = (NPCShirt) Random.Range(0,shirtPrefabs.Length);
		npcItems.pantsID = (NPCPants) Random.Range(0,pantPrefabs.Length);

		npcItems.hairColor = GenerateColor();
		npcItems.skinColor = (Random.Range(0.0f, 1.0f) >= 0.5f) ? GenerateColor(new Color(1f, 0.8f, 0.6f)) : GenerateColor(new Color(0.44f, 0.26f, 0.12f));
		npcItems.shirtColor = GenerateColor();
		npcItems.pantsColor = GenerateColor();

		if(hairPrefabs.Length > 0){
			GameObject temp = Instantiate(hairPrefabs[(int)npcItems.hairID], new Vector3(0,0,0), Quaternion.identity);
			temp.GetComponent<Renderer>().material.color = npcItems.hairColor;
		}
		if(earPrefabs.Length > 0){
			GameObject temp = Instantiate(earPrefabs[(int)npcItems.earsID], new Vector3(0,0,0), Quaternion.identity);
			temp.GetComponent<Renderer>().material.color = npcItems.skinColor;
		}
		if(nosePrefabs.Length > 0){
			GameObject temp = Instantiate(nosePrefabs[(int)npcItems.noseID], new Vector3(0,0,0), Quaternion.identity);
			temp.GetComponent<Renderer>().material.color = npcItems.skinColor;
		}
		if(shirtPrefabs.Length > 0){
			GameObject temp = Instantiate(shirtPrefabs[(int)npcItems.shirtID], new Vector3(0,0,0), Quaternion.identity);
			temp.GetComponent<Renderer>().material.color = npcItems.shirtColor;
		}
		if(pantPrefabs.Length > 0){
			GameObject temp = Instantiate(pantPrefabs[(int)npcItems.pantsID], new Vector3(0,0,0), Quaternion.identity);
			temp.transform.parent = this.transform;
			temp.GetComponent<Renderer>().material.color = npcItems.pantsColor;
		}


	}
	
	// Update is called once per frame
	void Update () {
		if(goingToWave){
			if(Vector3.Distance(playerObject.transform.position, this.transform.position) <= waveDistanceThreshold){
				//Player is within "Waving distance"
				isWaving = true;
				//the NPC is walking at slow speed to try and flag the player down.
				this.transform.position = (this.transform.position + new Vector3(0,0, -npcSlowSpeed * Time.deltaTime));
			} else {
				if(goingToWaveAtPlayer){
					//Make Eye Contact if going to wave at player
				} else if(goingToWave && !goingToWaveAtPlayer){
					//Eye Contact with other thing
				}
				//the NPC is walking at normal speed 
				this.transform.position = (this.transform.position + new Vector3(0,0, -npcSpeed * Time.deltaTime));
			}
		} else {
			//the NPC is not going to wave so doesn't slow down walking at normal speed 
			this.transform.position = (this.transform.position + new Vector3(0,0, -npcSpeed * Time.deltaTime));
		}

		if(this.transform.position.z < playerObject.transform.position.z - 10){
			Destroy(this.transform.gameObject);
		}
		
	}

	public Color GenerateColor(){
		return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
	}

	public Color GenerateColor(Color hint){
		return new Color(hint.r + Random.Range(-0.06f, 0.06f), hint.g + Random.Range(-0.06f, 0.06f), hint.b + Random.Range(-0.06f, 0.06f));
	}
}

public enum NPCHair{
	SHORT_WILD,
	SHORT_STRAIGHT,
	SHORT_CURLY,
	LONG_WILD,
	LONG_STRAIGHT,
	LONG_CURLY
}

public enum NPCEars{
	POINTY,
	POINTY_ATTACHED,
	ROUND,
	ROUND_ATTACHED,
}

public enum NPCNose{
	LONG_POINTY,
	LONG_ROUND,
	WIDE_POINTY,
	WIDE_ROUND,
	SHORT_POINTY,
	SHORT_ROUND
}

public enum NPCShirt{
	T_SHIRT,
	SUIT_WITH_TIE,
	SUIT_NO_TIE,
	SUIT_OPENED
}

public enum NPCPants{
	PANTS,
	JEANS,
	SUIT,
	SHORTS,
	SKIRT,
	DRESS,
	UNDERWEAR
}
