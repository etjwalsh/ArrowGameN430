using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //keeps track of the main stats for the game
    //these will be the stats that the upgrades mess with
    public float playerHealth = 100; //how much HP the player has
    public float maxPlayerHealth = 25; //max amount of HP the player can have
    public float playerDamage = 100; //how much damage the player does  
    public float dmgMultiplier = 1; //damage multiplier from upgrades
    public int maxArrows = 1; //how many arrows the player can shoot at once
    public float defence = 0; //player defence stat
    public float healthMult = 1; //health multiplier from upgrades
    public float powerScale = 1; //how quickly the arrow draws back
    public float maxPower = 100; //max bow power
    public float playerCoins = 0; //Coins
    public float coinMult = 1; //coin mult from upgrades
    public float moreCoins = 0; //addative coin amount from upgrades
    public float difficulty = 1; //overall game difficulty
    public Dictionary<string, int> upgradeLevels = new Dictionary<string, int>(); //list for upgrade names
    public int currentSceneIndex = 0;
    public int multiShotLevel = 0;


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
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerCoins += 100;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Cave");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("Mountain");
        }
        //move to the upgrade scene when the player dies
        if (playerHealth <= 0)
        {
            Debug.Log("player is dead now");
            SceneManager.LoadScene("UpgradeScene");
            playerHealth = maxPlayerHealth;
        }
    }

    //adds coins with allowance for the global mult, then adds any addative coin amounts
    public void addCoins(float amount)
    {
        playerCoins += (amount * coinMult) + moreCoins;
    }

    public int GetUpgradeLevel(string upgradeID)
    {
        if (upgradeLevels.ContainsKey(upgradeID))
            return upgradeLevels[upgradeID];
        return 0;
    }

    public void SetUpgradeLevel(string upgradeID, int level)
    {
        if (upgradeLevels.ContainsKey(upgradeID))
            upgradeLevels[upgradeID] = level;
        else
            upgradeLevels.Add(upgradeID, level);
    }
}
