using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] enemyPrefabs;
    public List<GameObject> enemyList;


    //modify these to actual boundaries
    float minX =-5.0f;
    float maxX =5.0f;
    float minY= -5.0f;
    float maxY = 5.0f;

    public float spawnInterval;
    public float intervalVariance;
    public float spawnDelay;
    //object with animation to run
    public GameObject spawnAnimation;

    void Start() {
        //grabs prefabs from "Assets/Enemies"
        enemyList = new List<GameObject>();
        StartCoroutine("spawnEnemy");
    }

    IEnumerator spawnEnemy() {
        while (true) {
            //set conditions for stopping spawner when player is dead
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            //instantiate animation at position with SpawnDelay using coroutine
            //StartCoroutine(waitAnimation(spawnDelay, position));

            GameObject toAdd = Instantiate(enemy, position, Quaternion.identity);
            toAdd.GetComponent<Enemy>().enemyManager = this;
            enemyList.Add(toAdd);

            yield return new WaitForSeconds(Random.Range(spawnInterval - intervalVariance, spawnInterval + intervalVariance));
        }

    }

    IEnumerator waitAnimation(float waitTime, Vector3 position) {
        Instantiate(spawnAnimation, position, Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
    }

}
