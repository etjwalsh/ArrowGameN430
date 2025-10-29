using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MommaBatController : MonoBehaviour
{
    public Arrow arrowScript;
    public float value;
    public float batHP = 3;
    public float batMaxHP = 3;
    public float hpPercent;
    [SerializeField] private hpBar healthDisplay;
    private Vector3 targetPosition;
    public float flyInDuration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(35, 30, 50);
        Vector3 pos = this.transform.position;
        pos = new Vector3(35, 200, 100);
        this.transform.position = pos;

        StartCoroutine(FlyIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (batHP <= 0)
        {
            //eventually make this so that it starts some sort of end sequence coroutine probably

            //add coin value to player's coins
            GameManager.instance.addCoins(value);

            //change to the end of the game scene!
            Destroy(gameObject);
            SceneManager.LoadScene("End");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Arrow(Clone)")
        {
            //get reference to this arrow script
            arrowScript = collision.gameObject.GetComponent<Arrow>();
            //make the bat take damage
            batHP -= 1;

            //update health bar UI
            hpPercent = batHP / batMaxHP;
            healthDisplay.currentHp = hpPercent;

            Debug.Log("bat health is now " + batHP);
        }
    }

    IEnumerator FlyIn()
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        //move the momma bat towards the target position slowly
        while (elapsed < flyInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / flyInDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition; // snap exactly to target
    }
}
