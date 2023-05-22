using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ShakeCamera : MonoBehaviour
{
    [Foldout("Transforms")]
    [SerializeField] private Transform cameraTransform;
    private Vector3 originalCamPos;
    [SerializeField] private float waitBeforeShake;
    [SerializeField] private float shakeTime;
    
    [SerializeField] private float shakeFrequency;
    [SerializeField] private AnimationCurve shakeCurve;

    private void Start() {
        originalCamPos = cameraTransform.position;
        StartCoroutine(WaitBeforeShake());
        
    }

    IEnumerator WaitBeforeShake()
    {
        StopCoroutine(Shake());
        yield return new WaitForSeconds(waitBeforeShake);
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        StopCoroutine(WaitBeforeShake());
        float timeElapsed = 0f;

        while(timeElapsed < shakeTime)
        {
            float shakeCurveFrequency = shakeCurve.Evaluate(timeElapsed / shakeTime) * shakeFrequency;
            cameraTransform.position = originalCamPos + Random.insideUnitSphere * shakeCurveFrequency;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        StopShake();
        StartCoroutine(WaitBeforeShake());
    }

    private void StopShake()
    {
        cameraTransform.position = originalCamPos;
    }

}
