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
        maxHealth = GameManager.instance.maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = GameManager.instance.playerHealth;
        redBar.fillAmount = currentHp / maxHealth;
    }
}
