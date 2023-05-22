using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class EnableVignette : MonoBehaviour
{
    [SerializeField] private float timeToFade = 0.25f;
    [SerializeField] private float timeToFadeColor = 1.75f;
    [SerializeField] private Volume postProcess;
    [SerializeField] private Color colorToFade;

    private bool fadeIn;
    private Vignette _vignette;
    private float defaultIntensity = 0f;
    private Color defaultColor;

    /*private void OnEnable()
    {
        fadeIn = true;
        postProcess.profile.TryGet<Vignette>(out _vignette);

        defaultIntensity = _vignette.intensity.value;
        defaultColor = _vignette.color.value;

        UIManager.Instance.StartFade(defaultIntensity, _vignette, defaultColor);
    }*/

    IEnumerator vignetteFade()
    {
        float timeElapsed = 0f;

        if(fadeIn)
        {
            while(timeElapsed < timeToFade)
            {
                _vignette.color.value = Color.Lerp(defaultColor, colorToFade, timeElapsed / timeToFadeColor);
                _vignette.intensity.value = Mathf.Lerp(defaultIntensity, 1, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while(timeElapsed < timeToFade)
            {
                _vignette.color.value = Color.Lerp(colorToFade, defaultColor, timeElapsed / timeToFadeColor);
                _vignette.intensity.value = Mathf.Lerp(1, defaultIntensity, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        fadeIn = !fadeIn;
    }



}
