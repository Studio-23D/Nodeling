﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> music = new List<AudioClip>();
    [SerializeField] private int currentIndex = 0;
    private bool pause = false;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	private void Start()
    {
        currentIndex = Random.Range(0, music.Count);
        audioSource.clip = music[currentIndex];
        audioSource.Play();
    }

    public void OtherSong(int step)
    {
        currentIndex += step;
        
        if(currentIndex >= music.Count)
        {
            currentIndex = 0;
        }
        else if (currentIndex <= -1)
        {
            currentIndex = music.Count - 1;
        }

        audioSource.clip = music[currentIndex];
        audioSource.Play();
    }

    public void ToggleMusic()
    {
        pause = !pause;

        if (pause)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
