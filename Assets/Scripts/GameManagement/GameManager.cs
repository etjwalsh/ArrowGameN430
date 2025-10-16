using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //keeps track of the main stats for the game
    //these will be the stats that the upgrades mess with
    public int playerHealth = 100; //how much HP the player has
    public int playerDamage = 10; //how much damage the player does
    public int maxArrows = 1; //how many arrows the player can shoot at once
    public int powerScale = 200; //how quickly the arrow draws back
    public int maxPower = 100; //max bow power

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

    }
}
