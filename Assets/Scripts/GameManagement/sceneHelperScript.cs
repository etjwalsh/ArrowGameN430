using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneHelperScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null)
        {
            return;
        }
        GameManager.instance.currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
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
