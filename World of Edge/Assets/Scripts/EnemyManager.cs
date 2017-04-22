using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    List<GameObject> enemyList;


    //modify these to actual boundaries
    float minX =-5.0f;
    float maxX =5.0f;
    float minY= -5.0f;
    float maxY = 5.0f;

    public float spawnInterval;
    public float intervalVariance;
    public float spawnDelay;

    void Start() {
        //grabs prefabs from "Assets/Enemies"
        enemyList = new List<GameObject>(Resources.LoadAll<GameObject>("Enemies"));
        StartCoroutine("spawnEnemy");
    }

    IEnumerator spawnEnemy() {
        while (true) {
            //set conditions for stopping spawner when player is dead
            GameObject enemy = enemyList[Random.Range(0, enemyList.Count)];
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            //instantiate animation at position with SpawnDelay using coroutine
            Instantiate(enemy, position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(spawnInterval - intervalVariance, spawnInterval + intervalVariance));
        }
    }

}
