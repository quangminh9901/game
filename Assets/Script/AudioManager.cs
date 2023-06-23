using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject prefab;
    public AudioClip player;
    private AudioSource playersource;
    public AudioClip score;
    private AudioSource scoresource;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        if (clip == this.player)
        {
            Play(clip, ref playersource, volume);
            return;
        }
        if (clip == this.score)
        {
            Play(clip, ref scoresource, volume);
            return;
        }
    }

    private void Play(AudioClip clip, ref AudioSource audioSource, float volume, bool isLoopback = false)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            return;
        }
        audioSource = Instantiate(instance.prefab).GetComponent<AudioSource>();

        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void StopSound(AudioClip clip)
    {
        if (clip == this.player)
        {
            playersource?.Stop();
            return;
        }
        if (clip == this.score)
        {
            scoresource?.Stop();
            return;
        }
    }
}
