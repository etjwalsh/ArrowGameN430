/*ENEMY SPAWN QUEUE DESCRIPTION

    In order for this queue to work, enemy spawn waves must be created in the unity inspector itself and MANUALLY entered into the queue
    This queue will not have any predefined enemy waves to spawn in. Everything MUST be manually assigned for this to work
    This allows for complete control from the developers over the difficulty of the game at any given time

    The enemiesToSpawn array field in each EnemyWave object within the queue must be populated for this to work, otherwise the queue will simply skip over that whole wave

    The queue will always spawn the FIRST element of the array, so when populating the list in the inspector make sure that element 0 is the earliest wave

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue : MonoBehaviour
{
    [SerializeField] public List<EnemyWave> enemySpawn; //queue for enemy spawn waves
    private Vector3 baseSpawn1;
    private Vector3 baseSpawn2;
    private Vector3 baseSpawn3;
    private GameObject enemyToSpawn;
    private EnemyWave thisWave;

    //actually spawn enemies in 
    private void Start()
    {
        //start spawning the first wave
        StartCoroutine(SpawnWave());
    }

    private EnemyWave GetWave()
    {
        //check if the spawn queue is empty, if it is give an error
        if (enemySpawn.Count == 0)
        {
            Debug.LogWarning("nothing to spawn");
            return null;
        }
        else //spawn queue is populated, dequeue first element
        {
            thisWave = enemySpawn[0]; //save current wave to a temp variable
            enemySpawn.RemoveAt(0); //remove the wave from the list
            return thisWave; //return the wave (temp variable)
        }
    }

    IEnumerator SpawnWave()
    {
        //get the current wave
        EnemyWave currentWave = GetWave();

        //loop through the array stored in the current wave
        for (int i = 0; i < currentWave.enemiesToSpawn.Length; i++)
        {
            enemyToSpawn = currentWave.enemiesToSpawn[i]; //get reference to the enemy you're about to spawn
            //instantiate the first enemy in the array, on the certain level, at a random horizontal location
            if (currentWave.spawnLevel == 1)
            {
                //set the random spawn spot
                baseSpawn1 = new Vector3(Random.Range(-20, 20), -5, 14);
                //spawn the enemy at spawn row 1
                Instantiate(enemyToSpawn, baseSpawn1, enemyToSpawn.transform.rotation);
            }
            else if (currentWave.spawnLevel == 2)
            {
                //set the random spawn point
                baseSpawn2 = new Vector3(Random.Range(-35, 35), -1.6f, 36);
                //spawn the enemy at spawn row 2
                Instantiate(enemyToSpawn, baseSpawn2, enemyToSpawn.transform.rotation);
            }
            else if (currentWave.spawnLevel == 3)
            {
                //set the random spawn spot
                baseSpawn3 = new Vector3(Random.Range(-30, 30), 2.8f, 66);
                //spawn the enemy at spawn row 3
                Instantiate(enemyToSpawn, baseSpawn3, enemyToSpawn.transform.rotation);
            }
            else
            {
                Debug.LogError("spawn level of wave " + currentWave + " is set incorrectly (must be 1, 2, or 3)");
            }

            //wait for currentWave.spawnBuffer seconds
            if (currentWave.spawnBuffer != -1) //make sure that the spawn buffer is actually set to an amount of time
            {
                yield return new WaitForSeconds(currentWave.spawnBuffer);
            }
            else
            {
                Debug.LogError("spawnBuffer of wave " + currentWave + " is not set");
            }

            // Debug.Log("got end of for loop at current wave " + currentWave + " index " + i);
        }

        // Debug.Log("got out of the for loop and thisWave == " + thisWave);
        //take some time between waves
        if (thisWave.waveBuffer != -1) //make sure that the wave buffer is actually set
        {
            yield return new WaitForSeconds(thisWave.waveBuffer);
        }
        else //give an error if it's not set properly
        {
            Debug.LogError("spawnBuffer of wave " + thisWave + " is not set");
        }

        // Debug.Log("got after the freaking one if/else thing");

        //start the next wave
        if (!thisWave.endOfSpawns) //make sure there are still waves to spawn
        {
            //call the next wave to spawn
            StartCoroutine(SpawnWave());
        }
    }
}
