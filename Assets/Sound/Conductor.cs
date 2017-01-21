using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour {
	private static Conductor _instance;
	public AudioSource[] Tracks;
	public float[] TrackMixinPoints;
	public float[] TrackVolumes;
	public float TrackTime = 43.875f;

	[RangeAttribute(0, 1)]
	public float MasterVolume = 1.0f;

	void Awake(){
		_instance = this;
		Play();
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
		for(int i=0; i<Tracks.Length; i++){
			AudioSource track = Tracks[i];
			track.volume = GetVolumeLevelForTrack(i, confidence) * TrackVolumes[i] * MasterVolume / Tracks.Length;
		}
	}

	private float GetVolumeLevelForTrack(int trackIndex, float confidence){
		float mixin = TrackMixinPoints[trackIndex];
		float range = 1.0f - mixin;
		float ret = (confidence - mixin) / range;

		return Mathf.Clamp(ret, 0, 1);
	}
}
