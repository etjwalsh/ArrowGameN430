using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Formats.Alembic.Importer;

public class MommaBatController : MonoBehaviour
{
    //stats and other stuff
    public Arrow arrowScript;
    public float value;
    public float batHP = 3.0f;
    public float batMaxHP = 3.0f;
    public int lastHP;
    public float hpPercent;
    [SerializeField] private hpBar healthDisplay;
    private bool canBeHit = false;

    //for flying in
    private Vector3 targetPosition;
    public float flyInDuration = 10.0f;

    //for bat children
    [SerializeField] private GameObject bat;
    private List<GameObject> batChildren = new List<GameObject> { };
    // private int numBats = -1;
    public float distanceInFront = 5f;
    public float spacing = 1f;
    public float arcAngle = 60f;
    public float circleRadius = 2f;

    //For tracking phases
    private int currentPhase = 3;

    //for eye animation
    public AlembicStreamPlayer ASP;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(35, 30, 50);
        Vector3 pos = new Vector3(35, 90, 450);
        transform.position = pos;

        //make sure the eye is closed
        ASP.CurrentTime += Time.deltaTime;
        if (ASP.CurrentTime < ASP.EndTime)
        {
            ASP.CurrentTime = ASP.EndTime;
        }

        //start the boss flying towards the player
        StartCoroutine(FlyIn());
    }

    // Update is called once per frame
    void Update()
    {
        //check if the bat is dead, move to the next scene
        if (batHP <= 0)
        {
            //eventually make this so that it starts some sort of end sequence coroutine probably

            //add coin value to player's coins
            GameManager.instance.addCoins(value);

            //change to the end of the game scene!
            Destroy(gameObject);
            SceneManager.LoadScene("Mountain");
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

            //Update current phase
            currentPhase = (int)batHP;

            //update health bar UI
            // Debug.Log("batHP: " + batHP);
            // Debug.Log("batMaxHP: " + batMaxHP);
            hpPercent = batHP / batMaxHP;
            // Debug.Log("hpPercent: " + hpPercent);
            healthDisplay.currentHp = hpPercent;
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

        //start the phases of the boss fight
        StartCoroutine(MommaBatPhases());
    }

    IEnumerator MommaBatPhases()
    {
        //loop forever
        while (batHP > 0)
        {
            //check what HP the momma bat is on (int 3-2-1-0)
            switch (currentPhase)
            {
                //the bat has 1 hp left
                case 1:
                    {
                        //spawn the third phase of bats (bat circle)
                        SpawnCircle(5);
                        break;
                    }
                //the bat has 2 hp left
                case 2:
                    {
                        //spawn the second phase of bats (bat arc)
                        SpawnArc(4);
                        break;
                    }
                //the bat has 3 hp left
                case 3:
                    {
                        //spawn the first phase of bats (bat line)
                        SpawnLine(3);
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

            //make the boss able to be hit
            canBeHit = true;

            //make the bat open its eye
            //make sure the eye is closed
            ASP.CurrentTime -= Time.deltaTime;
            if (ASP.CurrentTime > ASP.StartTime)
            {
                ASP.CurrentTime = ASP.StartTime;
            }

            //allow the boss to get hit for 2 seconds only
            yield return new WaitForSeconds(2f);

            //make the boss unable to be hit
            canBeHit = false;

            //close the eye
            //make sure the eye is closed
            ASP.CurrentTime += Time.deltaTime;
            if (ASP.CurrentTime < ASP.EndTime)
            {
                ASP.CurrentTime = ASP.EndTime;
            }
        }
    }

    private void SpawnLine(int numBats)
    {
        for (int i = 0; i < numBats; i++)
        {
            //get spawn position
            Vector3 spawnPos = new Vector3(transform.position.x - Random.Range(20, 40), transform.position.y - 20, transform.position.z - 35);
            float radius = 3f;
            int maxAttempts = 10;
            int attempts = 0;

            while (Physics.CheckSphere(spawnPos, radius) && attempts < maxAttempts)
            {
                Debug.Log("can't spawn here");
                attempts++;
                spawnPos = new Vector3(transform.position.x - Random.Range(20, 50), transform.position.y - 20, transform.position.z - 35);
            }

            //spawn in a line at spawn position
            GameObject spawnedBat = Instantiate(bat, spawnPos, transform.rotation * Quaternion.Euler(0, 180, 0), transform);

            //add the new bat to the list
            batChildren.Add(spawnedBat);
        }
    }

    private void SpawnArc(int numBats)
    {
        for (int i = 0; i < numBats; i++)
        {
            //get spawn position
            Vector3 spawnPos = new Vector3(transform.position.x - Random.Range(20, 40), transform.position.y - 20, transform.position.z - 35);
            float radius = 3f;
            int maxAttempts = 10;
            int attempts = 0;

            while (Physics.CheckSphere(spawnPos, radius) && attempts < maxAttempts)
            {
                Debug.Log("can't spawn here");
                attempts++;
                spawnPos = new Vector3(transform.position.x - Random.Range(20, 50), transform.position.y - 20, transform.position.z - 35);
            }

            //spawn in a line at spawn position
            GameObject spawnedBat = Instantiate(bat, spawnPos, transform.rotation * Quaternion.Euler(0, 180, 0), transform);

            //add the new bat to the list
            batChildren.Add(spawnedBat);
        }
    }

    private void SpawnCircle(int numBats)
    {
        for (int i = 0; i < numBats; i++)
        {
            //get spawn position
            Vector3 spawnPos = new Vector3(transform.position.x - Random.Range(20, 40), transform.position.y - 20, transform.position.z - 35);
            float radius = 3f;
            int maxAttempts = 10;
            int attempts = 0;

            while (Physics.CheckSphere(spawnPos, radius) && attempts < maxAttempts)
            {
                Debug.Log("can't spawn here");
                attempts++;
                spawnPos = new Vector3(transform.position.x - Random.Range(20, 50), transform.position.y - 20, transform.position.z - 35);
            }

            //spawn in a line at spawn position
            GameObject spawnedBat = Instantiate(bat, spawnPos, transform.rotation * Quaternion.Euler(0, 180, 0), transform);

            //add the new bat to the list
            batChildren.Add(spawnedBat);
        }
    }
}
