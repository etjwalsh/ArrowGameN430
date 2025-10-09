using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHp : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHp;
    [SerializeField] private Image redBar;

    // Start is called before the first frame update
    void Start()
    {
        //declare variables depending on what upgrades the player has used
        maxHealth = GameManager.instance.playerHealth;
        currentHp = GameManager.instance.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHp -= 10 * Time.deltaTime;

        redBar.fillAmount = (currentHp / maxHealth);

        if (currentHp <= 0)
        {
            SceneManager.LoadScene("UpgradeScene");
        }
    }
}
