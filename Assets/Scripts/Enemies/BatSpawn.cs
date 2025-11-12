using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class BatSpawn : MonoBehaviour
{
    public Arrow arrowScript;
    public float value;
    public float batHP = 200;
    public float batMaxHP = 200;
    public float hpPercent;
    public int min = 1;
    public int max = 10;
    [SerializeField] private hpBar healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        pos.y += UnityEngine.Random.Range(min, max);
        this.transform.position = pos;
    }

    void Update()
    {
        // Debug.Log("gamemanager instance maxPower = " + GameManager.instance.maxPower);
        if (batHP <= 0)
        {
            GameManager.instance.addCoins(value);
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


            hpPercent = batHP / batMaxHP;
            healthDisplay.currentHp = hpPercent;

            Debug.Log("bat health is now " + batHP);
        }
    }
}
