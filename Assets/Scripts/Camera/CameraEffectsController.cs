using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffectsController : MonoBehaviour {
    private static PlayerController player;

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

    public static float IrisTime = 2.0f;
    private static float IrisStartTime;
    private static bool irisMovingIn = true;
    private static bool irisActive = false;

    void Awake(){
        _instance = this;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        NoiseEffect = GetComponent<NoiseAndGrain>();
        Saturation = GetComponent<ColorCorrectionCurves>();
        Vignette = GetComponent<VignetteAndChromaticAberration>();
    }

    void Start(){
        IrisIn();
    }

    void Update(){
        if(irisActive){
            ApplyIrisEffect();
        }
    }

    public static void ApplyConfidenceToEffects(float Confidence){
        NoiseEffect.intensityMultiplier = Mathf.Lerp(_instance.NoiseMin, _instance.NoiseMax, Confidence);
        Saturation.saturation = Mathf.Lerp(_instance.SaturationMin, _instance.SaturationMax, Confidence);
        Vignette.chromaticAberration = Mathf.Lerp(_instance.ChromaMin, _instance.ChromaMax, 1.0f - Confidence);

        if(!irisActive){
            Vignette.intensity = Mathf.Lerp(_instance.VignetteMin, _instance.VignetteMax, 1.0f - Confidence);
        }

        Shader vignette = Vignette.vignetteShader;
    }

    public static void IrisIn(){
        Vignette.intensity = 1;
        StartIrisEffect(true);
    }

    public static void IrisOut(){
        StartIrisEffect(false);
    }

    public static void StartIrisEffect(bool movingIn){
        IrisStartTime = Time.time;
        irisMovingIn = movingIn;
        irisActive = true;
    }

    private static void ApplyIrisEffect(){
        float timeElapsed = Mathf.Clamp(Time.time - IrisStartTime, 0.0001f, IrisTime);
        float intensity = timeElapsed / IrisTime;
        float Confidence = player.GetConfidence();
        float confidenceVignette = Mathf.Lerp(_instance.VignetteMin, _instance.VignetteMax, 1.0f - Confidence);
        if(irisMovingIn){
            intensity = Mathf.Max(confidenceVignette, 1 - intensity);
        }

        Vignette.intensity = intensity;
        if(intensity >= 1 || intensity <= 0 || intensity == confidenceVignette){
            FinishIrisEffect();
        }
    }

    private static void FinishIrisEffect(){
        irisActive = false;
    }
}
