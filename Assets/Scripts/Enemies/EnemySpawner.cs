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

        // switch (whichEnemy)
        // {
        //     case 1:
        //         Instantiate(enemies[whichEnemy], spawnLocation.position, spawnLocation.rotation); //spawn a short target enemy
        //         whichEnemy++;
        //         break;
        //     case 2:
        //         Instantiate(targetEnemyTall, spawnLocation.position, spawnLocation.rotation); //spawn a tall target enemy
        //         whichEnemy++;
        //         break;
        //     case 3:
        //         Instantiate(batEnemy, spawnLocation.position, spawnLocation.rotation); //spawn a bat enemy
        //         whichEnemy = 1;
        //         break;

        // }
        // if (whichEnemy)
        // {
        //     Instantiate(targetEnemy, spawnLocation.position, spawnLocation.rotation); //spawn a target enemy
        //     whichEnemy = false; //flip the bool
        // }
        // else
        // {
        //     Instantiate(targetEnemy, spawnLocation.position, spawnLocation.rotation); //spawn a bat enemy
        //     whichEnemy = true; //flip the bool
        // }
    }
}
