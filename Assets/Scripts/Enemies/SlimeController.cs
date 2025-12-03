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
    [Header("Stats")]
    [SerializeField] public float value;
    [SerializeField] public float slimeHPScaling = 200;
    [SerializeField] public float damage;
    public float slimeMaxHP => slimeHPScaling * GameManager.instance.difficulty;
    public float slimeHP;
    public float hpPercent;
    public int min = 1;
    public int max = 10;
    [SerializeField] private hpBar healthDisplay;
    [SerializeField] private hitWheel hw;

    [Header("Bounce Settings")]
    [SerializeField] private float normalBounceMin;
    [SerializeField] private float normalBounceMax;
    [SerializeField] private float highBounceMin;
    [SerializeField] private float highBounceMax;
    [SerializeField] private float bounceSpeedMin;
    [SerializeField] private float bounceSpeedMax;
    [SerializeField] private float highBounceChance = 0.2f; //20% chance

    //animation stuff
    private Vector3 originalScale;
    private Vector3 startPosition;
    private bool isGrounded = true;
    private Rigidbody rb;
    private float gravityMultiplier = 2.0f;

    void Start()
    {
        slimeHP = slimeMaxHP;
        originalScale = transform.localScale;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        StartCoroutine(JumpAround());
    }
    void FixedUpdate()
    {
        // Apply custom gravity
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        //check if it got hit by an arrow
        if (collision.gameObject.name == "Arrow(Clone)")
        {
            //get reference to this arrow script
            arrowScript = collision.gameObject.GetComponent<Arrow>();
            //make the bat take damage
            slimeHP -= arrowScript.damage;

            hpPercent = slimeHP / slimeMaxHP;
            healthDisplay.currentHp = hpPercent;

            Debug.Log("slime health is now " + slimeHP);
        }

        //check if it hit the ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    void Update()
    {
        if (slimeHP <= 0)
        {
            GameManager.instance.addCoins(value);
            Destroy(gameObject);
        }
        if (hw.hitTime >= hw.hitRate)
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

    private IEnumerator JumpAround()
    {
        while (true)
        {
            //Wait until we're on the ground
            yield return new WaitUntil(() => isGrounded);

            //Small delay before bouncing
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

            // Decide if this is a high bounce
            bool isHighBounce = Random.value < highBounceChance;
            float bounceHeight;
            if (isHighBounce)
            {
                bounceHeight = Random.Range(highBounceMin, highBounceMax);
            }
            else
            {
                bounceHeight = Random.Range(normalBounceMin, normalBounceMax);
            }

            // Apply upward force
            float bounceForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * bounceHeight * rb.mass) * Random.Range(bounceSpeedMin, bounceSpeedMax);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

            isGrounded = false;

            // Wait a bit before checking for ground again
            yield return new WaitForSeconds(0.2f);
        }
    }
}
