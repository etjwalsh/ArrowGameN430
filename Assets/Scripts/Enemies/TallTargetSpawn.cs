using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallTargetSpawn : MonoBehaviour
{
    public float targetHP;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        //move the target to the correct position
        Vector3 pos = this.transform.position;
        pos.y += 3.0f;
        this.transform.position = pos;
    }
    void Update()
    {
        // Debug.Log("gamemanager instance maxPower = " + GameManager.instance.maxPower);
        // if (targetHP <= 0)
        // {
        //     Destroy(gameObject);
        //     GameManager.instance.addCoins(value);
        // }
    }
}
