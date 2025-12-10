using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneHelperScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float difficultySetting = 1f;
    void Start()
    {
        if (GameManager.instance == null)
        {
            return;
        }
        GameManager.instance.currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.difficulty = difficultySetting;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance == null)
        {
            return;
        }
    }
}
