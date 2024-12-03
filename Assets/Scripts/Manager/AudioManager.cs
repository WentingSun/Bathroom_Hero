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

    public AudioMixerSnapshot bathroom;

    public AudioMixerSnapshot desert;

    public AudioMixerSnapshot concert;

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
            sound.Source.loop = sound.loop;
        }

        foreach (var sound in FixedSoundsList)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;
            sound.Source.volume = sound.volumn;
            sound.Source.pitch = sound.pitch;
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
                Sound sound = Array.Find(Sounds, sound => sound.Name == soundName);
                sound.Source.Play();
            }
        }else if(watchingState == PlayerState.playerWatchMirror){
            foreach(string soundName in currentPlayingSoundName){
                Sound sound = Array.Find(FixedSoundsList, sound => sound.Name == soundName);
                sound.Source.Play();
            }
        }


    }

    private void audioManagerOnGameStateChange(GameState gameState)
    {
        // throw new System.NotImplementedException();
    }

    private void audioManagerOnPlayerStateChange(PlayerState playerState /*SelectedItem selectedItem*/)
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
            Sound dreamsound = Array.Find(Sounds, sound => sound.Name == "Dream_World");
            if (selectedItem == PlayerState.playerSelectMop)
            {
                dreamsound.Source.Play();
                concert.TransitionTo(0.5f);
            }
            else if (selectedItem == PlayerState.playerSelectTubelight)
            {
                dreamsound.Source.Play();
                desert.TransitionTo(0.5f);
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
