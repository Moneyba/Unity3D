﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music };

    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }

    AudioSource sound2DSource;
    public AudioSource[] musicSources;
    int activeMusicSourceIndex;

    public static AudioManager instance;


    SoundLibrary library;
   

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }

            GameObject newSound2DSource = new GameObject("2D sound source");
            sound2DSource = newSound2DSource.AddComponent<AudioSource>();
            newSound2DSource.transform.parent = transform;

          
            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
        }
    }




    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);

        }
    }

    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(soundName), pos);
    }

    
    public void PlaySound2D(string soundName)
    {
        sound2DSource.PlayOneShot(library.GetClipFromName(soundName), musicVolumePercent * masterVolumePercent);
        
    }

    

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1/duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1-activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
            yield return null;
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.Save();
    }
}
