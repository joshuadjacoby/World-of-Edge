using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerAI : Enemy {

    // Use this for initialization
    private NavMeshAgent agent;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
        enemyType = (int)enemyTypes.RUNNERAI;
    }
	
	// Update is called once per frame
	void Update () {
        agent.destination = player.transform.position;
        DoUpdate(Time.deltaTime);
    }
}
