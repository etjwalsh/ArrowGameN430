using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemies = 5;
    private int numEnemies;
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform spawnLocation;
    private int whichEnemy = 3;
    private bool started = false;
    private float radius = 3f;



    [SerializeField] private Queue<GameObject> enemyQueue = new Queue<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (numEnemies <= maxEnemies && !started)
        {
            started = true;
            for (int i = 0; i < maxEnemies; i++)
            {
                StartCoroutine(SpawnEnemy());
                numEnemies++;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);
        Debug.Log("got in here");

        if (whichEnemy > 2)
        {
            whichEnemy = 0; //reset which enemy back to the beginning
        }
        if (whichEnemy <= 2)
        {
            UnityEngine.Vector3 spawnPos = new UnityEngine.Vector3();
            int attempts = 0;
            int maxAttempts = 10;

            Debug.Log("spawnLocation.position is: " + spawnLocation.position);

            while (Physics.CheckSphere(spawnLocation.position, radius) && attempts < maxAttempts)
            {
                attempts++;
                spawnPos = spawnLocation.position + new UnityEngine.Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                Debug.Log("attempts: " + attempts);
            }

            //spawn the enemy at the index position of the enemy array
            Instantiate(enemies[whichEnemy], spawnPos, spawnLocation.rotation * UnityEngine.Quaternion.Euler(0, 90, 0));
            whichEnemy++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
