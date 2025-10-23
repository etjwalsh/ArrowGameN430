using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hitWheel : MonoBehaviour
{
    private float hitTime;
    [SerializeField] private float hitTimer;
    [SerializeField] private Image greenWheel;
    [SerializeField] private int maxTime;
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        hitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if the timer ran out / ended
        if (hitTime >= hitTimer)
        {
            //hit the player for damage amount
            GameManager.instance.playerHealth -= damage;

            //reset the timer
            hitTime = 0;
        }
        else
        {
            hitTime += maxTime * Time.deltaTime;
        }
        greenWheel.fillAmount = hitTime / hitTimer;
    }
}
