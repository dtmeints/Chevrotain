using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFX : MonoBehaviour
{
    public Light2D affectedLight;

    [Header("Intensity FX")]
    [Header("Pulse")]
    public bool pulse;
    public float pulseAmplitude;
    public float pulseFrequency;
    public float intensityMidpoint;

    [Header("Flicker")]
    public bool flicker;
    [Range(0, 10f)] public float flickerSpeedMod;
    public float flickerIntensity;
    public float flickerIntensityMidpoint;


    [Header("Color FX")]
    public Color colorA;
    public Color colorB;

    [Header("Color Pulse")]
    public bool colorPulse;
    public float colorPulseFrequency;

    [Header("Color Flicker")]
    public bool colorFlicker;
    
    [Range(0, 10f)] public float colorFlickerSpeedMod;

    private void Awake()
    {
        affectedLight = GetComponent<Light2D>();
    }


    private void Update()
    {
        if (pulse) affectedLight.intensity = Mathf.Sin(Time.time * pulseFrequency) * pulseAmplitude + intensityMidpoint;
        if (flicker) affectedLight.intensity = Mathf.PerlinNoise(Time.time * flickerSpeedMod, .5f) * flickerIntensity + flickerIntensityMidpoint;
        if (colorFlicker) affectedLight.color = Color.Lerp(colorA, colorB, Mathf.PerlinNoise(Time.time * colorFlickerSpeedMod, .5f));
        if (colorPulse) affectedLight.color = Color.Lerp(colorA, colorB, Mathf.Sin(Time.time * colorPulseFrequency) * .5f + .5f);
    }
}
