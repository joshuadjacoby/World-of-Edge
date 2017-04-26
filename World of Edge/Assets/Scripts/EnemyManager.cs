using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public float[] probabilites;
    public Transform[] spawnLocations;
    public float spawnInterval;
    public float intervalVariance;
    public int waveSize;
    public int enemiesToSpawn;
    public bool playerDead;
    public float spawnTimeReduction;

    private float spawnTimer = 0;
    public List<GameObject> enemyList;

    void Start()
    {
        enemiesToSpawn = waveSize;
    }

    void Update()
    {
        if (playerDead)
        {
            foreach (GameObject enemy in enemyList)
            {
                Destroy(enemy);
            }
        }
        else
        {
            if (enemiesToSpawn > 0)
            {
                if (spawnTimer > 0)
                {
                    spawnTimer -= Time.deltaTime;
                }
                else
                {
                    spawnTimer = spawnInterval - spawnTimeReduction + Random.Range(0, intervalVariance);
                    --enemiesToSpawn;

                    // Pick a number between 0 and 1 inclusive, then iteratively sum up the
                    // probabilities defined in the list. Assume they sum to 1.
                    float randomFloat = Random.Range(0f, 1f);
                    int probabilityIndex = 0;
                    float probability = 0;
                    if (probabilites.Length > 0)
                    {
                        probability = probabilites[0];
                        while (probabilityIndex < probabilites.Length && randomFloat > probability)
                        {
                            ++probabilityIndex;
                            probability += probabilites[probabilityIndex];
                        }
                    }

                    GameObject toAdd = Instantiate(enemyPrefabs[probabilityIndex]);
                    toAdd.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].position;
                    toAdd.GetComponent<Enemy>().enemyManager = this;
                    enemyList.Add(toAdd);
                }
            }
        }
    }

    public int EnemiesLeftToKill()
    {
        return enemiesToSpawn + enemyList.Count;
    }
}
