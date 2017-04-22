using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TurretMoveAI : Enemy {

    // Use this for initialization
    private NavMeshAgent agent;
    //get enemy list
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        health = 100;
        damage = 10;
        agent.destination 
	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = player.transform.position;
    }
    private void getNearestEnemy()
    {

    }
}
