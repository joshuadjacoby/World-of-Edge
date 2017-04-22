using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GluerAI : MonoBehaviour {
    NavMeshAgent agent;
    GameObject player;
    List<GameObject> enemies;

    public EnemyManager enemyManager;



	void Start () {
        enemies = enemyManager.enemyList;
        agent = GetComponent<NavMeshAgent>();
        moveToClosest();
	}
	
    void moveToClosest() {
        GameObject closest = findClosest();
        agent.destination = closest.transform.position;
    }

    GameObject findClosest() {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject enemy in enemies) {
            float currDistance = Vector3.Distance(position, enemy.transform.position);
            if (currDistance < distance) {
                distance = currDistance;
                closest = enemy;
            }
        }
        return closest;
    }

}
