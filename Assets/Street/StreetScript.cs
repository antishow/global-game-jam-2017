using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetScript : MonoBehaviour {

	 
	public int BlockSize;
	public int StreetCount;
	public int SideStreetCount;
	public GameObject Sidewalk;
	public GameObject Road;
	public GameObject Building;
	public GameObject RoadIntersection;
	
	// Use this for initialization
	void Start ()
	{
		BuildStreet(StreetCount);
	}


	// Update is called once per frame
	void Update ()
	{

	}
	private void BuildStreet (int blocks)
	{
		for (int i = 0; i < blocks; i++)
		{
			BuildBlock(i);

		}
	}

	private void BuildBlock (int b)
	{
		for (int i = 0; i < BlockSize - 4; i++)
		{
			BuildSection(b, i);
		}
		BuildSideStreet(b );
	}

	private void BuildSideStreet (int b)
	{
		SideStreetA((b* BlockSize) + BlockSize - 4);
		SideStreetB((b * BlockSize) + BlockSize - 2.5f);
		SideStreetA((b * BlockSize) + BlockSize - 1);
		SideStreetC((b * BlockSize) + BlockSize);

	}

	private void SideStreetC (int b)
	{
		for (int i = 0; i < SideStreetCount; i++)
		{
			float B1_size = (UnityEngine.Random.Range(-490, 490)) / 100;
			float B2_size = (UnityEngine.Random.Range(-490, 490)) / 100;

			Instantiate(Building, new Vector3(-4-i, B1_size, b  ), Quaternion.identity);
			Instantiate(Building, new Vector3(1+i, B2_size, b  ), Quaternion.identity);
		}
	}

	private void SideStreetB (float b)
	{
		Instantiate(RoadIntersection, new Vector3(-1.5f, 0, b), Quaternion.identity);

		for (int i = 0; i < SideStreetCount; i++)
		{
			Instantiate(Road, new Vector3(-3 - i, 0, b), Quaternion.Euler(0,90,0));
			Instantiate(Road, new Vector3(i, 0, b), Quaternion.Euler(0, 90, 0));
		}
	}

	private void SideStreetA (int b)
	{
		Instantiate(Road, new Vector3(-1.5f, 0, b), Quaternion.identity);
		
		for (int i = 0; i < SideStreetCount; i++)
		{
			Instantiate(Sidewalk, new Vector3(-3 - i, 0, b), Quaternion.identity);
			Instantiate(Sidewalk, new Vector3(i, 0, b), Quaternion.identity);
		}
	}

	private void BuildSection (int b, int i)
	{
		float B1_size =( UnityEngine.Random.Range(-490, 490))/100;
		float B2_size = (UnityEngine.Random.Range(-490, 490))/100;

		Instantiate(Building, new Vector3(-4, B1_size, b * BlockSize + i), Quaternion.identity);
		Instantiate(Building, new Vector3(1, B2_size, b * BlockSize + i), Quaternion.identity);
		Instantiate(Sidewalk, new Vector3(-3, 0, b * BlockSize + i), Quaternion.identity);
		Instantiate(Sidewalk, new Vector3(0, 0, b * BlockSize + i), Quaternion.identity);
		Instantiate(Road, new Vector3(-1.5f, 0, b * BlockSize + i), Quaternion.identity);
	}
}

