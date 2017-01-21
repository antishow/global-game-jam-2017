using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffectsController : MonoBehaviour {
    public static NoiseAndGrain NoiseEffect;
    public static ColorCorrectionCurves Saturation;
    private static CameraEffectsController _instance;

    public float NoiseMin;
    public float NoiseMax;
    public float SaturationMin;
    public float SaturationMax;

    void Awake(){
        _instance = this;
        NoiseEffect = GetComponent<NoiseAndGrain>();
        Saturation = GetComponent<ColorCorrectionCurves>();
    }

    public static void ApplyConfidenceToEffects(float Confidence){
        NoiseEffect.intensityMultiplier = Mathf.Lerp(_instance.NoiseMin, _instance.NoiseMax, Confidence);
        Saturation.saturation = Mathf.Lerp(_instance.SaturationMin, _instance.SaturationMax, Confidence);
    }
}
