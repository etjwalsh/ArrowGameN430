using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MommaBatController : MonoBehaviour
{
    //stats and other stuff
    public Arrow arrowScript;
    public float value;
    public float batHP = 3;
    public float batMaxHP = 3;
    public float hpPercent;
    [SerializeField] private hpBar healthDisplay;
    private bool canBeHit = false;

    //for flying in
    private Vector3 targetPosition;
    public float flyInDuration = 10.0f;

    //for bat children
    [SerializeField] private GameObject bat;
    private int numBats;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(35, 30, 50);
        Vector3 pos = this.transform.position;
        pos = new Vector3(35, 90, 450);
        this.transform.position = pos;

        StartCoroutine(FlyIn());
    }

    // Update is called once per frame
    void Update()
    {
        //check if all the bats are dead
        if (numBats == 0)
        {
            canBeHit = true;
        }
        else if (numBats < 0)
        {

        }
        else
        {
            //set the number of bats to however many children the mamma bat has
            numBats = gameObject.transform.childCount;
        }

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
        //check to make sure the mamma got hit by an arrow and can actually be hit
        if (collision.gameObject.name == "Arrow(Clone)" && canBeHit)
        {
            //can't hit it multiple times in a row
            canBeHit = false;

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
            transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }

        transform.position = targetPosition; // snap exactly to target
    }

    IEnumerator SpawnChildren()
    {
        //instantiate bats as children of the momma bat
        Instantiate(bat, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        numBats++;
        yield return null;
    }
}
