using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds = new();
    private Dictionary<string, Sound> soundsDic = new();

    void Awake()
    {
        InitializeSounds();
    }

    private void InitializeSounds()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.audio;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            sound.audioSource = source;
            soundsDic[sound.name] = sound;
        }
    }

    public void Play(string name)
    {
        if (soundsDic.TryGetValue(name, out Sound sound))
        {
            sound.audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found!");
        }
    }

    public int GetMusicVolume()
    {
        foreach (var sound in sounds)
        {
            if (sound.audioType == AudioType.Music)
            {
                return (int)(sound.volume * 100);
            }
        }
        return 0;
    }

    public int GetSoundVolume()
    {
        foreach (var sound in sounds)
        {
            if (sound.audioType == AudioType.Sound)
            {
                return (int)(sound.volume * 100);
            }
        }
        return 0;
    }

    public void SetMusicVolume(int volume)
    {
        float normalizedVolume = volume / 100f;

        foreach (var sound in sounds)
        {
            if (sound.audioType == AudioType.Music)
            {
                sound.volume = normalizedVolume;
                if (sound.audioSource != null)
                {
                    sound.audioSource.volume = normalizedVolume;
                }
            }
        }
    }

    public void SetSoundVolume(int volume)
    {
        float normalizedVolume = volume / 100f;

        foreach (var sound in sounds)
        {
            if (sound.audioType == AudioType.Sound)
            {
                sound.volume = normalizedVolume;
                if (sound.audioSource != null)
                {
                    sound.audioSource.volume = normalizedVolume;
                }
            }
        }
    }
}
