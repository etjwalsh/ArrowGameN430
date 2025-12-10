using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    public List<AudioClip> musicTracks;
    private bool started = false;
    private bool menuStarted = false;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "UpgradeScene")
        {
            music.volume = 0.35f;
        }
        else
        {
            music.volume = 1.0f;
        }

        if (scene.name == "MainMenu" || scene.name == "Intro")
        {
            music.clip = musicTracks[0];

            if(!menuStarted)
            {
                music.Play();
                menuStarted = true;
            }
        }
        else
        {
            music.clip = musicTracks[1];
            if (!started)
            {
                music.Play();
                started = true;
            }
        }
    }
}
