using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]//so that wave structure will be able to show up inside unity inspector
    public class Wave//So basically each Wave will have the following things
    {
        public Enemy[] enemies; //Contains all enemies that can spawn
        public int count;//Decides how many enemies will spawn inside a wave
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;//All possible points enemies can spawn from
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);//Time between spawning next wave
        StartCoroutine(SpawnWave(index));//Spawns wave
    }

    IEnumerator SpawnWave (int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)//if players dead this whole process stops
            {
                yield break;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if(i == currentWave.count - 1)//Makes sure every enemy in each wave spawns
            {
                finishedSpawn = true;
            }
            else
            {
                finishedSpawn = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if (finishedSpawn == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)//If all enemies have spawned and all enemies are dead
        {
            finishedSpawn = false;
            if(currentWaveIndex + 1< waves.Length)//makes sure a new wave even exists
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else//no more waves to spawn so
            {
                Debug.Log("GameFinished!!");
            }
        }
    }
}
