using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    [SerializeField] private AudioSource source;
    private void Start()
    {
        Instance = this;
    }
    public void PlaySound(AudioClip clip, float volume, float pitch)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip, volume);
    }

}
