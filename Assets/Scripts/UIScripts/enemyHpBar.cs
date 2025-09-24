using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    private float maxHealth = 100;
    [SerializeField] private float currentHp;
    [SerializeField] private Image greenBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        greenBar.fillAmount = (currentHp / maxHealth);
    }
}
