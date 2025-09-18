using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hitWheel : MonoBehaviour
{
    private float hitTime;
    [SerializeField] private float hitTimer;
    [SerializeField] private Image greenWheel;

    // Start is called before the first frame update
    void Start()
    {
        hitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hitTime >= hitTimer)
        {
            hitTime = 0;
        }
        else
        {
            hitTime += 20 * Time.deltaTime;
        }
        greenWheel.fillAmount = (hitTime / hitTimer);
    }
}
