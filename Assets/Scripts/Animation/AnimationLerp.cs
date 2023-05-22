using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AnimationLerp : MonoBehaviour
{
    [SerializeField] private Light2D pointLight;

    [SerializeField] private float minIntensity = 0.05f;
    [SerializeField] private float maxIntensity = 0.15f;
    [SerializeField] private float intensityLerpSpeed = 1f;
    private float defaultIntensity;
    private float newIntensity;
    private float randomIntensity;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light2D>();
        pointLight.intensity = minIntensity;
        defaultIntensity = pointLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        newIntensity = defaultIntensity * Mathf.Abs(Mathf.Cos(Time.time * intensityLerpSpeed) + maxIntensity);
        pointLight.intensity = newIntensity;
    }
}
