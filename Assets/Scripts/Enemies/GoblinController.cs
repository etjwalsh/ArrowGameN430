using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    //stats
    public Arrow arrowScript;
    [Header("Stats")]
    [SerializeField] public float value;
    [SerializeField] public float goblinHPScaling = 200;
    [SerializeField] public float damage;
    [SerializeField]  public float attackRate = 4f;
    public float goblinMaxHP => goblinHPScaling * GameManager.instance.difficulty;
    private float goblinHP;
    public float hpPercent;
    public int min = 1;
    public int max = 10;

    [Header("UI")]
    [SerializeField] private hpBar healthDisplay;
    [SerializeField] private hitWheel hw;

    //for little hitting animation
    private float attackDuration = 0.1f;
    private Vector3 attackSpot;


    // Start is called before the first frame update
    void Start()
    {
        goblinHP = goblinMaxHP;
        hw.damage = damage;
        hw.hitRate = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (goblinHP <= 0)
        {
            GameManager.instance.addCoins(value);
            Destroy(gameObject);
        }
        if (hw.hitTime >= hw.hitRate)
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
            goblinHP -= arrowScript.damage;

            hpPercent = goblinHP / goblinMaxHP;
            healthDisplay.currentHp = hpPercent;

            Debug.Log("goblin health is now " + goblinHP);
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
