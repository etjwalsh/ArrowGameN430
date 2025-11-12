using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MommaBatController : MonoBehaviour
{
    //stats and other stuff
    public Arrow arrowScript;
    public float value;
    public int batHP = 3;
    public int batMaxHP = 3;
    public int lastHP;
    public float hpPercent;
    [SerializeField] private hpBar healthDisplay;
    private bool canBeHit = false;

    //for flying in
    private UnityEngine.Vector3 targetPosition;
    public float flyInDuration = 10.0f;

    //for bat children
    [SerializeField] private GameObject bat;
    private List<GameObject> batChildren = new List<GameObject> { };
    private int numBats;
    public float distanceInFront = 5f;
    public float spacing = 1f;
    public float arcAngle = 60f;
    public float circleRadius = 2f;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new UnityEngine.Vector3(35, 30, 50);
        UnityEngine.Vector3 pos = this.transform.position;
        pos = new UnityEngine.Vector3(35, 90, 450);
        this.transform.position = pos;

        StartCoroutine(FlyIn());
    }

    // Update is called once per frame
    void Update()
    {
        //check if all the bats are dead
        if (numBats == 0)
        {
            // canBeHit = true;
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
        }
    }

    IEnumerator FlyIn()
    {
        UnityEngine.Vector3 startPosition = transform.position;
        float elapsed = 0f;

        //move the momma bat towards the target position slowly
        while (elapsed < flyInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / flyInDuration);
            transform.position = UnityEngine.Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }

        transform.position = targetPosition; // snap exactly to target
        StartCoroutine(MommaBatPhases());
    }

    IEnumerator MommaBatPhases()
    {
        while (true)
        {
            switch (batHP)
            {
                //the bat is dead
                case 0:
                    {
                        //tell the game manager to move the player to stage 2
                        break;
                    }
                //the bat has 1 hp left
                case 1:
                    {
                        //spawn the third phase of bats (bat circle)
                        SpawnCircle(6);
                        break;
                    }
                //the bat has 2 hp left
                case 2:
                    {
                        //spawn the second phase of bats (bat arc)
                        SpawnArc(5);
                        break;
                    }
                //the bat has 3 hp left
                case 3:
                    {
                        //spawn the first phase of bats (bat line)
                        SpawnLine(4);
                        break;
                    }
            }

            //wait until all spawned bats are destroyed
            while (batChildren.Count > 0)
            {
                //remove null entries (destroyed bats)
                batChildren.RemoveAll(b => b == null);
                yield return null;
            }

            while (batHP == lastHP)
            {
                canBeHit = true;
                yield return null;
            }

        }
    }

    private void SpawnLine(int numBats)
    {
        for (int i = 0; i < numBats; i++)
        {
            //get spawn position
            UnityEngine.Vector3 spawnPos = new UnityEngine.Vector3(transform.position.x - Random.Range(20, 40), transform.position.y - 20, transform.position.z - 35);
            float radius = 3f;
            int maxAttempts = 10;
            int attempts = 0;

            while (Physics.CheckSphere(spawnPos, radius) && attempts < maxAttempts)
            {
                Debug.Log("can't spawn here");
                attempts++;
                spawnPos = new UnityEngine.Vector3(transform.position.x - Random.Range(20, 50), transform.position.y - 20, transform.position.z - 35);
            }

            //spawn in a line at spawn position
            GameObject spawnedBat = Instantiate(bat, spawnPos, transform.rotation * UnityEngine.Quaternion.Euler(0, 180, 0), transform);

            //add the new bat to the list
            batChildren.Add(spawnedBat);
        }
    }

    private void SpawnArc(int numBats)
    {
        Debug.Log("spawn arc!");
    }

    private void SpawnCircle(int numBats)
    {
        Debug.Log("spawn circle!");
    }
}
