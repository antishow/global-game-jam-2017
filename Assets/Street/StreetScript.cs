using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetScript : MonoBehaviour
{ 
	public int BlockSize;
	public int StreetCount;
	 
	public GameObject Sidewalk;
	public GameObject CrossStreet;
	public GameObject[] Buildings;
	public GameObject RoadIntersection;
	public List<GameObject> StreetParts;

	  int BuildingSize = 16;
	public int FullStreetLength = 0;
	public int CurrentLoopCheck = 10;

 
	// Use this for initialization
	void Start ()
	{ 
		BuildStreet(StreetCount);
		FullStreetLength = StreetCount * BlockSize*BuildingSize;
	}


	// Update is called once per frame
	void Update ()
	{
		CheckLoop();
	}

	private void CheckLoop ()
	{ float zBuffer = BuildingSize;
        int CurrentZ =Mathf.FloorToInt(Camera.main.transform.position.z );
		Debug.Log(CurrentZ + " " + CurrentLoopCheck);
		if (CurrentLoopCheck+zBuffer < CurrentZ )//stuff thats 2 behind player
		{
			CurrentLoopCheck = CurrentZ;
			MoveItemsBehindPlayer(CurrentZ- zBuffer);
		}

	}

	private void MoveItemsBehindPlayer (float Waterline)
	{
		 
		foreach (GameObject p in StreetParts)
		{ Vector3 pPosition = p.transform.position;
            if (pPosition.z<Waterline)
			{
				pPosition.z += FullStreetLength;
				p.transform.position = pPosition;
			}
			
		}
	}

	private void BuildStreet (int streets)
	{
		int HalfBlock = Mathf.FloorToInt( streets / 2); 
        for (int i = 0; i < streets; i++)
		{
			BuildBlock(i); 
		}
		 
		foreach (GameObject s in StreetParts)
		{ 
			s.transform.parent = this.transform;
		}
	}
	 

	private void BuildBlock (int b)
	{
		for (int i = 0; i < BlockSize -1; i++)
		{
			BuildSection(b, i);
		}
		BuildSideStreet(b);
	}

	private void BuildSideStreet (int block)
	{
		SideStreetA((block * BlockSize)+ (BlockSize-1) );
		SideStreetB((block * BlockSize) + BlockSize  );

	}
	private void SideStreetA (float b)
	{
		StreetParts.Add(Instantiate(CrossStreet, new Vector3(0, 0, b* BuildingSize), Quaternion.identity));
	}	 

	private void SideStreetB (float b)
	{
		for (int i = 0; i < 7; i ++)
		{
			int B1 = (UnityEngine.Random.Range(0, Buildings.Length));
			int B2 = (UnityEngine.Random.Range(0, Buildings.Length));
			float zLoc = (b   * 16);
			StreetParts.Add(Instantiate(Buildings[B1], new Vector3((i+1)* BuildingSize, 0, zLoc), Quaternion.Euler(-90, 0, -90)));
			GameObject GO = Instantiate(Buildings[B2], new Vector3((-3- i)* BuildingSize, 0, zLoc), Quaternion.Euler(-90, 0, -90));
			Vector3 SCALE = GO.transform.localScale;
			SCALE.y *= -1;
			GO.transform.localScale = SCALE;
			StreetParts.Add(GO);
		}

	}

	

	private void BuildSection (int b, int i)
	{
		int B1  = (UnityEngine.Random.Range(0, Buildings.Length))  ;
		int B2  = (UnityEngine.Random.Range(0, Buildings.Length))  ;
		float zLoc =( b * BlockSize + i)* BuildingSize;
        StreetParts.Add(Instantiate(Buildings[B1], new Vector3(0, 0, zLoc), Quaternion.Euler(-90,0,-90)));
		GameObject GO = Instantiate(Buildings[B2], new Vector3(-32, 0, zLoc), Quaternion.Euler(-90, 0, -90));
		Vector3 SCALE = GO.transform.localScale;
		SCALE.y *= -1;
		GO.transform.localScale = SCALE;
		StreetParts.Add(GO);
		StreetParts.Add(Instantiate(Sidewalk, new Vector3(0, 0, zLoc), Quaternion.identity));
		 
	}
}

