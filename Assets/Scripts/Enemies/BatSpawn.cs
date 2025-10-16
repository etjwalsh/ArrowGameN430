using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatSpawn : MonoBehaviour
{
    public Arrow arrowScript;
    public int batHP = 200;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        pos.y += Random.Range(1, 10);
        this.transform.position = pos;
    }

    void Update()
    {
        // Debug.Log("gamemanager instance maxPower = " + GameManager.instance.maxPower);
        if (batHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Arrow(Clone)")
        {
            //get reference to this arrow script
            arrowScript = collision.gameObject.GetComponent<Arrow>();
            //make the bat take damage
            batHP -= arrowScript.damage;

            Debug.Log("bat health is now " + batHP);
        }
    }
}
