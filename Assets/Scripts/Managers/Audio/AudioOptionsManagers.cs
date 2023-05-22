using UnityEngine;

public class AudioOptionsManagers : MonoBehaviour
{
    public static float mainVolume {get; private set;}
    public static float musicVolume {get; private set;}
    public static float soundEffectsVolume {get; private set;}
    public static float dialogueVolume {get; private set;}

    public void OnMainSliderValueChanged(float value)
    {
        mainVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnMusicSliderValueChanged(float value)
    {
        musicVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnSFXSliderValueChanged(float value)
    {
        soundEffectsVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
    }
    public void OnDialogueSliderValueChanged(float value)
    {
        dialogueVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
    }
}
