using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    public List<AudioClip> musicTracks;
    private bool started = false;

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

        if (scene.name == "MainMenu")
        {
            music.clip = musicTracks[0];
            music.Play();
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
