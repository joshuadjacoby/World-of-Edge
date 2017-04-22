using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GluerAI : Enemy {
    NavMeshAgent agent;
    GameObject player;




	void Start () {
        agent = GetComponent<NavMeshAgent>();
        moveToClosest();
	}
	
    void Update() {
        DoUpdate(Time.deltaTime);
    }
    void moveToClosest() {
        GameObject closest = findClosest();
        agent.destination = closest.transform.position;
    }


    GameObject findClosest() {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject enemy in enemyManager.enemyList) {
            float currDistance = Vector3.Distance(position, enemy.transform.position);
            if (currDistance < distance) {
                distance = currDistance;
                closest = enemy;
            }
        }
        return closest;
    }

}
