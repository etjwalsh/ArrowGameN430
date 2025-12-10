using UnityEngine;
using UnityEngine.SceneManagement;

public class BigGoblin : MonoBehaviour
{
    [SerializeField] private GoblinController bossGobScript;

    void Update()
    {
        if(bossGobScript.goblinHP <= 0)
        {
            SceneManager.LoadScene("Mountain");
        }
    }
}
