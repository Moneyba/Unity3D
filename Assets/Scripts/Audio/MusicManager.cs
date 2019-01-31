﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip menuTheme;
    public AudioClip mainTheme;
   
    string sceneName;

	// Use this for initialization
	void Start () {
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }
       
        OnLevelWasLoaded(0);
	}
   


    private void OnLevelWasLoaded(int level)
    {
        string newSceneName = SceneManager.GetActiveScene().name;
        if(newSceneName != sceneName )
        {
            sceneName = newSceneName;
            Invoke("PlayMusic", .2f);
        }
        
    }
  

    private void PlayMusic()
    {
        AudioClip clipToPlay = null;
        if(sceneName == "Menu")
        {
            clipToPlay = menuTheme;
        }else if(sceneName == "Main")
        {
            clipToPlay = mainTheme;

        }
       


        if (clipToPlay != null)
        {
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
        
    }

    
   
}
