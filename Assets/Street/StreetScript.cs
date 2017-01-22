using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetScript : MonoBehaviour
{
	Transform PlayerLocation;
	public int BlockSize;
	public int StreetCount;
	public int SideStreetCount;
	public GameObject Sidewalk;
	public GameObject Road;
	public GameObject Building;
	public GameObject RoadIntersection;
	public List<GameObject> StreetParts;  
 
	// Use this for initialization
	void Start ()
	{
		PlayerLocation = Camera.main.transform;
		BuildStreet(StreetCount);
	}


	// Update is called once per frame
	void Update ()
	{
		 
	}

	 

	private void BuildStreet (int streets)
	{
		int HalfBlock = Mathf.FloorToInt( streets / 2); 
        for (int i = 0; i < BlockSize; i++)
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
		for (int i = 0; i < BlockSize - 4; i++)
		{
			BuildSection(b, i);
		}
		BuildSideStreet(b);
	}

	private void BuildSideStreet (int b)
	{
		SideStreetA((b * BlockSize) + BlockSize - 4);
		SideStreetB((b * BlockSize) + BlockSize - 2.5f);
		SideStreetA((b * BlockSize) + BlockSize - 1);
		SideStreetC((b * BlockSize) + BlockSize);

	}

	private void SideStreetC (int b)
	{
		for (int i = 0; i < SideStreetCount; i+=2)
		{
			float B1_size = (UnityEngine.Random.Range(-490, 490)) / 100;
			float B2_size = (UnityEngine.Random.Range(-490, 490)) / 100;
			StreetParts.Add(Instantiate(Building, new Vector3(-6f - i, B1_size, b+.5f), Quaternion.identity));
			StreetParts.Add(Instantiate(Building, new Vector3(3.5f + i, B2_size, b+.5f), Quaternion.identity));
		}
	}

	private void SideStreetB (float b)
	{

		StreetParts.Add(Instantiate(RoadIntersection, new Vector3(-1.5f, 0, b), Quaternion.identity));

		for (int i = 0; i < SideStreetCount; i++)
		{
			StreetParts.Add(Instantiate(Road, new Vector3(-3 - i, 0, b), Quaternion.Euler(0, 90, 0)));
			StreetParts.Add(Instantiate(Road, new Vector3(i, 0, b), Quaternion.Euler(0, 90, 0)));
		}
	}

	private void SideStreetA (int b)
	{
		StreetParts.Add(Instantiate(Road, new Vector3(-1.5f, 0, b), Quaternion.identity));

		for (int i = 0; i < SideStreetCount; i++)
		{
			StreetParts.Add(Instantiate(Sidewalk, new Vector3(-3 - i, 0, b), Quaternion.identity));
			StreetParts.Add(Instantiate(Sidewalk, new Vector3(i, 0, b), Quaternion.identity));
		}
	}

	private void BuildSection (int b, int i)
	{
		float B1_size = (UnityEngine.Random.Range(-490, 490)) / 100;
		float B2_size = (UnityEngine.Random.Range(-490, 490)) / 100;
		if (i%2==0)
		{
			StreetParts.Add(Instantiate(Building, new Vector3(-4.5f, B1_size, b * BlockSize + i+.5f), Quaternion.identity));
			StreetParts.Add(Instantiate(Building, new Vector3(1.5f, B2_size, b * BlockSize + i + .5f), Quaternion.identity));
		}
		
		StreetParts.Add(Instantiate(Sidewalk, new Vector3(-3, 0, b * BlockSize + i), Quaternion.identity));
		StreetParts.Add(Instantiate(Sidewalk, new Vector3(0, 0, b * BlockSize + i), Quaternion.identity));
		StreetParts.Add(Instantiate(Road, new Vector3(-1.5f, 0, b * BlockSize + i), Quaternion.identity));
	}
}

