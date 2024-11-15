using UnityEngine;

public enum AudioType
{
    Music,
    Sound,
}


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audio;
    [Range(0f, 1f)] public float volume;
    [Range(-3f, 3f)] public float pitch;
    public bool loop;
    [HideInInspector] public AudioSource audioSource;
    public AudioType audioType = AudioType.Sound;
}
