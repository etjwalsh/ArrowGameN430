using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "Data/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    public GameObject[] enemiesToSpawn; //array of enemies that will spawn
    public float spawnBuffer = -1; //amount of time between spawning each individual enemy within the wave
    public float waveBuffer = -1; //amount of time between the end of this wave and the next wave starting
    public int spawnLevel; //number 1 through 3. The queue will recognize which of these it is and spawn the enemies randomly on one of the levels
    public bool endOfSpawns = false;
}
