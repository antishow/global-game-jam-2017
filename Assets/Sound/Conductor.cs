using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour {
	private static Conductor _instance;
	public AudioSource[] Tracks;
	public float[] TrackMixinPoints;
	public float[] TrackVolumes;

	void Awake(){
		_instance = this;
	}

	public static void MixForConfidence(float confidence){
		_instance.mixForConfidence(confidence);
	}

	protected void mixForConfidence(float confidence){
		for(int i=0; i<Tracks.Length; i++){
			AudioSource track = Tracks[i];
			track.volume = GetVolumeLevelForTrack(i, confidence) * TrackVolumes[i] / Tracks.Length;
		}
	}

	private float GetVolumeLevelForTrack(int trackIndex, float confidence){
		if(trackIndex == 0){
			return 1.0f;
		}

		float mixin = TrackMixinPoints[trackIndex];
		float range = 1.0f - mixin;
		float ret = (confidence - mixin) / range;

		return Mathf.Clamp(ret, 0, 1);
	}
}
