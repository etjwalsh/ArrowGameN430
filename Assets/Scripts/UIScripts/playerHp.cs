using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHp : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHp;
    [SerializeField] private Image redBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        redBar.fillAmount = (currentHp / maxHealth);
    }
}
