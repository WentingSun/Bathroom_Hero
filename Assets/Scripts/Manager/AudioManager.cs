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
        }

        foreach (var sound in FixedSoundsList)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;
            sound.Source.volume = sound.volumn;
            sound.Source.pitch = sound.pitch;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= audioManagerOnGameStateChange;
        GameManager.OnPlayerStateChage -= audioManagerOnPlayerStateChange;
    }

    private void audioManagerOnGameStateChange(GameState gameState)
    {
        // throw new System.NotImplementedException();
    }

    private void audioManagerOnPlayerStateChange(PlayerState playerState /*SelectedItem selectedItem*/)
    {
        if(playerState == PlayerState.playerWatchMirror || playerState == PlayerState.playerDontWatchMirror){
            watchingState = playerState;
        }else if(playerState == PlayerState.playerSelectTubelight || playerState == PlayerState.playerSelectMop || playerState == PlayerState.PlayerSelectNothing){
            selectedItem = playerState;
        }

        if (watchingState == PlayerState.playerWatchMirror)
        {
            Sound dreamsound = Array.Find(Sounds, sound => sound.Name == "Dream_World");
            if(selectedItem == PlayerState.playerSelectMop)
            {
                dreamsound.Source.Play();
                concert.TransitionTo(0.5f);
            }
            else if(selectedItem == PlayerState.playerSelectTubelight)
            {
                dreamsound.Source.Play();
                desert.TransitionTo(0.5f);
            }
            else{
                bathroom.TransitionTo(0.5f);
            }
        }
        else
        {
            bathroom.TransitionTo(0.5f);
        }

    }

    public void PlaySound(string soundName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.Name == soundName);
        if(watchingState == PlayerState.playerWatchMirror){

            
            sound = Array.Find(Sounds, sound => sound.Name == soundName);


        }else if(watchingState == PlayerState.playerDontWatchMirror){
            sound = Array.Find(FixedSoundsList, sound => sound.Name == soundName);
            Debug.Log("fixedSound.");
        }
        if (sound == null)
        {
            Debug.LogWarning("Audio name:" + soundName + "not found.");
            return;
        }
        sound.Source.Play();
    }

    public void PlaySound(string soundName, float pitch, float volume){
        PlaySound(soundName);
        Debug.Log("pitch: "+ pitch);
        Debug.Log("volume: "+ volume);
    }
    public void StopSound(string soundName){

    }

}
