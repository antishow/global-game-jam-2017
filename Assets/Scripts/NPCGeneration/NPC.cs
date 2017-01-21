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
	// Use this for initialization
	void Start () {
		npcItems.hairID = (NPCHair) Random.Range(0,5);
		npcItems.earsID = (NPCEars) Random.Range(0,3);
		npcItems.noseID = (NPCNose) Random.Range(0,5);
		npcItems.shirtID = (NPCShirt) Random.Range(0,3);
		npcItems.pantsID = (NPCPants) Random.Range(0,6);

		npcItems.hairColor = GenerateColor();
		npcItems.skinColor = (Random.Range(0.0f, 1.0f) >= 0.5f) ? GenerateColor(new Color(1f, 0.8f, 0.6f)) : GenerateColor(new Color(0.44f, 0.26f, 0.12f));
		npcItems.shirtColor = GenerateColor();
		npcItems.pantsColor = GenerateColor();
	}
	
	// Update is called once per frame
	void Update () {
		
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
