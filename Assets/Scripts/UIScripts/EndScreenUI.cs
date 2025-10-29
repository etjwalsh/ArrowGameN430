using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndScreenUI : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
    }

    public void OnExitClicked()
    {
        Application.Quit();

        // For testing in the Unity Editor
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
