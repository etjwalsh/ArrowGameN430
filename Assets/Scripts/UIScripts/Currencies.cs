using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Currencies : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI curText;
    private float coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null) {
        coins = GameManager.instance.playerCoins;
        curText.text = "Coins: " + coins.ToString();
        }
    }
}
