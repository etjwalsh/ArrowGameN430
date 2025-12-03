using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hitWheel : MonoBehaviour
{
    public float hitTime; //time since last hit
    public float hitRate; //attack rate seconds
    [SerializeField] private Image greenWheel;
    public float damage; //damage to deal to player on hit

    // Start is called before the first frame update
    void Start()
    {
        hitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance == null)
        {
            return;
        }
        //if the timer ran out / ended
        if (hitTime >= hitRate)
        {
            screenFlash.SpecialEffects.ScreenFlashEffect(0.1f);

            //hit the player for damage amount
            GameManager.instance.playerHealth -= damage;

            //reset the timer
            hitTime = 0;
        }
        else
        {
            //increment the timer
            hitTime += Time.deltaTime;
        }
        //update the wheel fill amount
        greenWheel.fillAmount = hitTime / hitRate;
    }
}
