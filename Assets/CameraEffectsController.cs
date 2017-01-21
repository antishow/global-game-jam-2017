using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffectsController : MonoBehaviour {
    public static NoiseAndGrain NoiseEffect;
    public static ColorCorrectionCurves Saturation;
    public static VignetteAndChromaticAberration Vignette;
    private static CameraEffectsController _instance;

    public float NoiseMin;
    public float NoiseMax;
    public float SaturationMin;
    public float SaturationMax;

    public float VignetteMin = 0.1f;
    public float VignetteMax = 0.25f;

    public float ChromaMin = 0.15f;
    public float ChromaMax = 0.4f;

    void Awake(){
        _instance = this;
        NoiseEffect = GetComponent<NoiseAndGrain>();
        Saturation = GetComponent<ColorCorrectionCurves>();
        Vignette = GetComponent<VignetteAndChromaticAberration>();
    }

    public static void ApplyConfidenceToEffects(float Confidence){
        NoiseEffect.intensityMultiplier = Mathf.Lerp(_instance.NoiseMin, _instance.NoiseMax, Confidence);
        Saturation.saturation = Mathf.Lerp(_instance.SaturationMin, _instance.SaturationMax, Confidence);
        Vignette.chromaticAberration = Mathf.Lerp(_instance.ChromaMin, _instance.ChromaMax, 1.0f - Confidence);
        Vignette.intensity = Mathf.Lerp(_instance.VignetteMin, _instance.VignetteMax, 1.0f - Confidence);

        Shader vignette = Vignette.vignetteShader;
    }

    public static void IrisIn(){
        print("Once upon a time, in a procedurally generated city....");
    }

    public static void IrisOut(){
        print("That's all, folks!");
    }
}
