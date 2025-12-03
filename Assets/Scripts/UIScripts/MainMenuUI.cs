using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    public void OnStartClicked()
    {
        //start the game
        SceneManager.LoadScene("Forest");
    }

    public void OnExitClicked()
    {
        //for official build of the game
        Application.Quit();

        // For testing in the Unity Editor
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    // public void OnSettingClicked()
    // {

    // }

    // public void OnCreditsClicked()
    // {

    // }
}
