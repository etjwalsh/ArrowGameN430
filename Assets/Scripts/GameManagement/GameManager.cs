using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //keeps track of the main stats for the game
    //these will be the stats that the upgrades mess with
    public int playerHealth = 100; //how much HP the player has
    public int maxPlayerHealth = 100; //max amount of HP the player can have
    public float playerDamage = 100; //how much damage the player does
    public int maxArrows = 1; //how many arrows the player can shoot at once
    public int powerScale = 200; //how quickly the arrow draws back
    public float maxPower = 100; //max bow power
    public float playerCoins = 0; //max bow power
    public float coinMult = 0; //max bow power
    public List<string> upgrades = new(); //list for upgrade names

    //singleton pattern
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        //set instance of state machine and make sure one doesn't already exist
        if (instance != null)
        {
            Debug.LogWarning("warning: too many instances of GameManager");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerHealth = 0;
        }
        //move to the upgrade scene when the player dies
        if (playerHealth <= 0)
        {
            Debug.Log("player is dead now");
            SceneManager.LoadScene("UpgradeScene");
            playerHealth = maxPlayerHealth;
        }
    }

//adds coins with allowance for the global mult
    public void addCoins(int amount)
    {
        playerCoins += amount * coinMult;
    } 
}
