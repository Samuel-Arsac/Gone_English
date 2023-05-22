using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Audio;
public class AudioManager : ProjectManager<AudioManager>
{
    [Header("Mixers")]
    [SerializeField] private AudioMixerGroup mainMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup SFXMixerGroup;
    [SerializeField] private AudioMixerGroup dialogueMixerGroup;
    
    [Space(5)]
    [Header("Tracks")]
    [SerializeField] private Sound[] musics;
    [SerializeField] private Sound[] dialogues;
    public Sound currentTrack;
    public Sound newTrack;

    protected override void Awake()
    {
        base.Awake();
        foreach (Sound s in musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;


            switch(s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = SFXMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;

                case Sound.AudioTypes.dialogue:
                    s.source.outputAudioMixerGroup = dialogueMixerGroup;
                    break;
            }
        }

        foreach(Sound s in dialogues)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;


            switch (s.audioType)
            {
                case Sound.AudioTypes.dialogue:
                    s.source.outputAudioMixerGroup = dialogueMixerGroup;
                    break;
            }
        }
    }

    // Update is called once per frame
  public void PlayMusic(string name)
    {
        currentTrack =  Array.Find(musics, sound => sound.name == name);
        if(currentTrack == null)
        {
            Debug.LogWarning("musics : " + name + " not found !");
            return;
        }
        currentTrack.source.Play();
    }

    public void SwapMusic(string name)
    {
        newTrack = Array.Find(musics, sound => sound.name == name);
        if(newTrack == null)
        {
            Debug.LogWarning("Musics : " + name + " not found !");
            return;
        }

        StartCoroutine(FadeTracks());
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(musics, sound => sound.name == name);
        if(sfx == null)
        {
            Debug.LogWarning("SFX : " + name + " not found !");
            return;
        }
        sfx.source.Play();
    }

    public void PlayDialogue(string name)
    {
        Sound dialogue = Array.Find(dialogues, sound => sound.name == name);
        if(dialogue == null)
        {
            Debug.LogWarning("Dialogue : " + name + " not found !");
            return;
        }
        dialogue.source.Play();
    }

    public IEnumerator Stop(string name)
    {
        Sound s = Array.Find(musics, sound => sound.name == name);
        float timeToFade = 0.75f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade)
        {
            s.source.volume = Mathf.Lerp(0,s.source.volume, timeElapsed/timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        s.source.Stop();
        //currentTrack = null;
    }

    IEnumerator FadeTracks()
    {
        float timeToFade = 0.75f;
        float timeElapsed = 0;

        newTrack.source.Play();

        while(timeElapsed < timeToFade)
        {
            newTrack.source.volume = Mathf.Lerp(0,newTrack.volume, timeElapsed/timeToFade);
            currentTrack.source.volume = Mathf.Lerp(currentTrack.volume,0, timeElapsed/timeToFade);
            timeElapsed+= Time.deltaTime;
            yield return null;
        }
        OnSwitchMusicEnd();

    }

    public void OnSwitchMusicEnd()
    {
        StopCoroutine(FadeTracks());
        currentTrack.source.Stop();
        currentTrack = newTrack;
        newTrack = null;
    }


    public void UpdateMixerVolume()
    {
        mainMixerGroup.audioMixer.SetFloat("Master", Mathf.Log10(AudioOptionsManagers.mainVolume) * 20);
        musicMixerGroup.audioMixer.SetFloat("Music", Mathf.Log10(AudioOptionsManagers.musicVolume) * 20);
        SFXMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(AudioOptionsManagers.soundEffectsVolume) * 20);
        dialogueMixerGroup.audioMixer.SetFloat("Dialogue", Mathf.Log10(AudioOptionsManagers.dialogueVolume) * 20);
    }
}
