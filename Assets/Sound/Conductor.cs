using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour {
	private static Conductor _instance;
	public AudioSource[] Tracks;
	public float[] TrackMixinPoints;
	public float[] TrackVolumes;
	public float TrackTime = 43.875f;

	private float StartTime;
	private bool FadingIn;

	[RangeAttribute(0, 1)]
	public float MasterVolume = 1.0f;

	private static bool IsPaused = false;
	private PlayerController player;

	void Awake(){
		_instance = this;
		StartTime = Time.time;
		FadingIn = true;
		Play();
		player = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	private void Update(){
		if(FadingIn){
			MasterVolume = Mathf.Clamp((Time.time - StartTime) / CameraEffectsController.IrisTime, 0, 1);
		}

		if(FadingIn && MasterVolume == 1){
			FadingIn = false;
		}
	}

	private void Play(){
		for(int i=0; i<Tracks.Length; i++){
			AudioSource track = Tracks[i];
			track.time = 0;
		}

		Invoke("Play", TrackTime);
	}

	public static void MixForConfidence(float confidence){
		_instance.mixForConfidence(confidence);
	}

	protected void mixForConfidence(float confidence){
		int i, l = Tracks.Length;
		for(i=0; i<l; i++){
			AudioSource track = Tracks[i];
			if(IsPaused){
				if(i==0){
					track.volume = Mathf.Min(0.15f, GetVolumeLevelForTrack(i, confidence) * TrackVolumes[i] * MasterVolume / Tracks.Length);
				} else {
					track.volume = 0;
				}
			} else {
				track.volume = GetVolumeLevelForTrack(i, confidence) * TrackVolumes[i] * MasterVolume / Tracks.Length;
			}
		}
	}

	public static void PauseDamper(){
		IsPaused = true;
		MixForConfidence(_instance.player.GetConfidence());
	}

	public static void UnpauseDamper(){
		IsPaused = false;
		MixForConfidence(_instance.player.GetConfidence());
	}

	private float GetVolumeLevelForTrack(int trackIndex, float confidence){
		float mixin = TrackMixinPoints[trackIndex];
		float range = 1.0f - mixin;
		float ret = (confidence - mixin) / range;

		return Mathf.Clamp(ret, 0, 1);
	}
}
