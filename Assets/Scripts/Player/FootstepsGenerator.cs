using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsGenerator : MonoBehaviour {
	public AudioClip[] Clips;
	public float FootstepPeriod = 1.0f;
	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource>();
	}

	void Start(){
		InvokeRepeating("PlayFootstep", FootstepPeriod, FootstepPeriod);
	}

	private void PlayFootstep(){
		source.clip = Clips[Random.Range(0, Clips.Length)];
		source.Play();
	}
}
