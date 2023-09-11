using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<Sound> sounds = new List<Sound>();
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.audio;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            sound.audioSource = source;
        }
    }


    public void Play(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.audioSource.Play();
                break;
            }
        }
    }
}
