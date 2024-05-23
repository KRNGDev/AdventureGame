using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeLightIntermitente : MonoBehaviour
{
    Light light;

    public float secondsBetweenTwinkle;
    public float duration;
    public float tkinkleIntensity;
    public Color twinkleColor;
    [Range(0,100)]
    public float probability;
    private float intensity;
    private Color color;

    void Start()
    {
        light = GetComponent<Light>();
        intensity = light.intensity;
        color = light.color;
        InvokeRepeating("Tkinkle", secondsBetweenTwinkle, secondsBetweenTwinkle);
    }

    void Tkinkle()
    {
        if (Random.Range(0,100)<=probability)
        {
            light.intensity = tkinkleIntensity;
            light.color = twinkleColor;
            Invoke("Restore", duration);
        }
    }
    void Restore()
    {
        light.intensity = intensity;
        light.color = color;
    }
}
