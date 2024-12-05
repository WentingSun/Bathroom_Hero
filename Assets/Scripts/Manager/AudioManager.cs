using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : Singleton<AudioManager>
{

    public Sound[] Sounds;
    public Sound[] FixedSoundsList;

    [SerializeField] private PlayerState watchingState;
    [SerializeField] private PlayerState selectedItem;

    [SerializeField] private Sound currentSound;

    public static AudioMixer audioMixer;

    public AudioMixerSnapshot bathroom;

    public AudioMixerSnapshot desert;

    public AudioMixerSnapshot concert;

    [HideInInspector]
    public int flag = 0;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChange += audioManagerOnGameStateChange;
        GameManager.OnPlayerStateChage += audioManagerOnPlayerStateChange;
        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;
            sound.Source.volume = sound.volumn;
            sound.Source.pitch = sound.pitch;
            sound.Source.outputAudioMixerGroup = sound.audioMixerGroup;
            sound.Source.loop = sound.loop;
        }

        foreach (var sound in FixedSoundsList)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;
            sound.Source.volume = sound.volumn;
            sound.Source.pitch = sound.pitch;
            sound.Source.outputAudioMixerGroup = sound.audioMixerGroup;
            sound.Source.loop = sound.loop;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= audioManagerOnGameStateChange;
        GameManager.OnPlayerStateChage -= audioManagerOnPlayerStateChange;
    }

    private void switchAudioSource()
    {
        List<string> currentPlayingSoundName = new List<string>();
        foreach(Sound sound in Sounds){
            if(sound.Source.isPlaying == true){
                currentPlayingSoundName.Add(sound.Name);
                sound.Source.Stop();
            }
        }
        foreach(Sound sound in FixedSoundsList){
            if(sound.Source.isPlaying == true){
                currentPlayingSoundName.Add(sound.Name);
                sound.Source.Stop();
            }
        }
        if(watchingState== PlayerState.playerDontWatchMirror){
            foreach(string soundName in currentPlayingSoundName){
                Sound sound = Array.Find(FixedSoundsList, sound => sound.Name == soundName);
                sound.Source.Play();
            }
        }else if(watchingState == PlayerState.playerWatchMirror){
            foreach(string soundName in currentPlayingSoundName){
                Sound sound = Array.Find(Sounds, sound => sound.Name == soundName);
                sound.Source.Play();
            }
        }


    }

    public static IEnumerator StartFade(string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

     private IEnumerator PlayDreamWorld()
    {
        Sound dreamsound = Array.Find(Sounds, sound => sound.Name == "Dream_World");
        dreamsound.Source.Play();
        yield return new WaitUntil(() => dreamsound.Source.time >= dreamsound.clip.length);
    }

    private void audioManagerOnGameStateChange(GameState gameState)
    {
        // throw new System.NotImplementedException();
    }

    private void audioManagerOnPlayerStateChange(PlayerState playerState)
    {
        
        if (playerState == PlayerState.playerWatchMirror || playerState == PlayerState.playerDontWatchMirror)
        {
            watchingState = playerState;
            switchAudioSource();
        }
        else if (playerState == PlayerState.playerSelectTubelight || playerState == PlayerState.playerSelectMop || playerState == PlayerState.PlayerSelectNothing)
        {
            selectedItem = playerState;
        }

        if (watchingState == PlayerState.playerWatchMirror)
        {
            
            if (selectedItem == PlayerState.playerSelectMop)
            {
                flag = 1;
                StartFade("bathroom_bg_volume", 3.0f, -80);
                StartCoroutine(PlayDreamWorld());
                concert.TransitionTo(2.0f);
            }
            else if (selectedItem == PlayerState.playerSelectTubelight)
            {
                flag = 1;
                StartFade("bathroom_bg_volume", 3.0f, -80);
                StartCoroutine(PlayDreamWorld());
                StartFade("desert_bg_volume", 3.0f, 10);
                desert.TransitionTo(3.0f);
            }
            else
            {
                bathroom.TransitionTo(0.5f);
            }
        }
        else
        {
            if (selectedItem == PlayerState.playerSelectMop)
            {
                if(flag == 1)
                {
                    StartCoroutine(PlayDreamWorld());
                    StartFade("bathroom_bg_volume", 3.0f, -14);
                    bathroom.TransitionTo(3.0f);
                    flag = 0;
                }
                else
                {
                    bathroom.TransitionTo(0.5f);
                }
            }
            else if(selectedItem == PlayerState.playerSelectTubelight)
            {
                if(flag == 1)
                {
                    StartFade("desert_bg_volume", 3.0f, -80);
                    StartCoroutine(PlayDreamWorld());
                    StartFade("bathroom_bg_volume", 3.0f, -14);
                    bathroom.TransitionTo(3.0f);
                    flag = 0;
                }
                else
                {
                    bathroom.TransitionTo(0.5f);
                }
            }
            else
            {
                bathroom.TransitionTo(0.5f);
            }

        }

    }

    private IEnumerator PlayClip(Sound sound)
    {
        currentSound = sound;
        sound.Source.Play();
        yield return null;
    }


    private Sound FindSound(string soundName)
    {
        Sound result;
        if (watchingState == PlayerState.playerWatchMirror)
        {
            result = Array.Find(Sounds, sound => sound.Name == soundName);
        }
        else if (watchingState == PlayerState.playerDontWatchMirror)
        {
            result = Array.Find(FixedSoundsList, sound => sound.Name == soundName);
        }
        else
        {
            result = null;
        }
        if (result == null)
        {
            Debug.LogWarning("Audio name:" + soundName + "not found.");
        }
        return result;
    }

    public void PlaySound(string soundName)
    {
        Sound sound = FindSound(soundName);

        if (currentSound.Source == null)
        {
            StartCoroutine(PlayClip(sound));
        }
        else
        {
            currentSound.Source.Stop();
            StartCoroutine(PlayClip(sound));
        }

    }

    public void PlaySound(string soundName, float pitch, float volume)
    {
        Sound sound = FindSound(soundName);
        if (currentSound.Name == soundName)
        {
            currentSound.Source.pitch = pitch;
            currentSound.pitch = pitch;
            currentSound.Source.volume = volume;
            currentSound.volumn = volume;
        }
        else
        {
            sound.Source.pitch = pitch;
            sound.pitch = pitch;
            sound.Source.volume = volume;
            sound.volumn = volume;
            sound.Source.Play();
        }

    }


    public void StopSound(string soundName)
    {
        if (currentSound.Name == soundName)
        {
            currentSound.Source.Stop();
        }
        else
        {
            Sound sound = FindSound(soundName);
            sound.Source.Stop();
        }
    }

}
