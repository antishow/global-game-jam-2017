using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour {
	public Transform spawnPosition;
	public float maxSpawnTime;
	public float minSpawnTime;
	public float currentTime;
	public GameObject npcPrefab;

	// Use this for initialization
	void Start () {
		spawnPosition = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime <= 0){
			if(npcPrefab != null){
				GameObject temp = Instantiate(npcPrefab, spawnPosition.position, Quaternion.identity);
				temp.transform.parent = this.transform;
			}
			currentTime = Random.Range(minSpawnTime, maxSpawnTime);
		} else {
			currentTime -= Time.deltaTime;
		}
	}
}
