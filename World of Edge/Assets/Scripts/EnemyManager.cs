using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    struct RandomSelection {
        private int enemyIndex;
        public float probability;
     
        public RandomSelection(int enemyIndex, float probability) {
            this.enemyIndex = enemyIndex;
            this.probability = probability;
        }
        public int GetValue() { return enemyIndex; }
    }
     


    public GameObject[] enemyPrefabs;
    public List<GameObject> enemyList;

    public float[] probabilites;
    float upperProbability;

    //modify these to actual boundaries
    float minX =-5.0f;
    float maxX =5.0f;
    float minY= -5.0f;
    float maxY = 5.0f;

    public float spawnInterval;
    public float intervalVariance;
    public float spawnDelay;
    public int waveSize;
    //object with animation to run
    public GameObject spawnAnimation;
    RandomSelection[] randomSelections;
    private bool playerDead;

    void Start() {
        //Stores probability weights for usage with getRandomEnemyIndex();
        randomSelections = new RandomSelection[enemyPrefabs.Length];
        for (int i = 0;i < enemyPrefabs.Length;i++){
            randomSelections[i] = new RandomSelection(i, probabilites[i]);
        }

        //grabs prefabs from "Assets/Enemies"
        enemyList = new List<GameObject>();
        StartCoroutine(spawnEnemies(waveSize,spawnInterval));
    }

    IEnumerator spawnEnemies(int waveSize, float spawnInterval) {
        while (enemyList.Count < waveSize) {
            //set conditions for stopping spawner when player is dead
            if (playerDead)
            {
                StopAllCoroutines();
                break;
            }

            int enemyIndex = GetRandomEnemyIndex(randomSelections);
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            //instantiate animation at position with SpawnDelay using coroutine
            //StartCoroutine(waitAnimation(spawnDelay, position));

            GameObject toAdd = Instantiate(enemyPrefabs[enemyIndex], position, Quaternion.identity);
            toAdd.transform.position = transform.position;
            toAdd.GetComponent<Enemy>().enemyManager = this;
            enemyList.Add(toAdd);

            yield return new WaitForSeconds(Random.Range(spawnInterval - intervalVariance, spawnInterval + intervalVariance));
        }
        //end the coroutine

    }

    IEnumerator waitAnimation(float waitTime, Vector3 position) {
        Instantiate(spawnAnimation, position, Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
    }

    int GetRandomEnemyIndex(params RandomSelection[] selections) {
        float rand = Random.value;
        float currentProb = 0;
        foreach (RandomSelection selection in selections) {
            currentProb += selection.probability;
            if (rand <= currentProb)
                return selection.GetValue();
        }
     
        //will happen if the input's probabilities sums to less than 1
        //throw error here if that's appropriate
        return -1;
    }

    public void playerIsDead(bool dead )
    {
        playerDead = dead;
    }

}
