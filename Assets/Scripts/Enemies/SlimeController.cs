using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    //for puff up animation
    [SerializeField] private float puffScale = 1.3f;
    [SerializeField] private float puffDuration = 0.2f;

    //stats
    public Arrow arrowScript;
    public float value;
    public float slimeHP = 200;
    public float slimeMaxHP = 200;
    public float hpPercent;
    public int min = 1;
    public int max = 10;
    [SerializeField] private hpBar healthDisplay;
    [SerializeField] private hitWheel hw;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Arrow(Clone)")
        {
            //get reference to this arrow script
            arrowScript = collision.gameObject.GetComponent<Arrow>();
            //make the bat take damage
            slimeHP -= arrowScript.damage;

            hpPercent = slimeHP / slimeMaxHP;
            healthDisplay.currentHp = hpPercent;

            Debug.Log("bat health is now " + slimeHP);
        }
    }

    void Update()
    {
        if (slimeHP <= 0)
        {
            GameManager.instance.addCoins(value);
            Destroy(gameObject);
        }
        if (hw.hitTime >= hw.hitTimer)
        {
            StartCoroutine(HitAnimation());
        }
    }

    private IEnumerator HitAnimation()
    {
        Debug.Log("starting hit");
        Vector3 targetScale = originalScale * puffScale;
        float elapsed = 0f;

        // Scale up
        while (elapsed < puffDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / puffDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        elapsed = 0f;

        // Scale back down
        while (elapsed < puffDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / puffDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
