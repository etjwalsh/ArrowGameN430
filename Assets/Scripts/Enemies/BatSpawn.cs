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
    [SerializeField] private hitWheel hw;

    //for little hitting animation
    private float attackDuration = 0.1f;
    private Vector3 attackSpot;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        pos.y += UnityEngine.Random.Range(min, max);
        transform.position = pos;
    }

    void Update()
    {
        // Debug.Log("gamemanager instance maxPower = " + GameManager.instance.maxPower);
        if (batHP <= 0)
        {
            GameManager.instance.addCoins(value);
            Destroy(gameObject);
        }
        if (hw.hitTime >= hw.hitTimer)
        {
            StartCoroutine(HitAnimation());
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

    IEnumerator HitAnimation()
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        attackSpot = startPosition - new Vector3(0, 0, 3);

        //lunge forward
        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / attackDuration);
            transform.position = Vector3.Lerp(startPosition, attackSpot, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }

        //lunge back immediately after
        elapsed = 0f;
        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / attackDuration);
            transform.position = Vector3.Lerp(attackSpot, startPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
