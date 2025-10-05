using System.Collections;
using System.Collections.Generic;
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

        if (whichEnemy > 2)
        {
            whichEnemy = 0; //reset which enemy back to the beginning
        }
        if (whichEnemy <= 2)
        {
            //spawn the enemy at the index position of the enemy array
            Instantiate(enemies[whichEnemy], spawnLocation.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), spawnLocation.rotation * Quaternion.Euler(0, 90, 0));
            whichEnemy++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
