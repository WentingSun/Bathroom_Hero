using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

    public string Name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volumn;
    [Range(.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource Source;
    public AudioMixerGroup audioMixerGroup;
    public bool loop;

}