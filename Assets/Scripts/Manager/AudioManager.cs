using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{

    public Sound[] Sounds;
    public Sound[] FixedSoundsList;
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

    private void audioManagerOnPlayerStateChange(PlayerState playerState)
    {
        // throw new System.NotImplementedException();
    }

    public void PlaySound(string soundName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.Name == soundName);
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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
