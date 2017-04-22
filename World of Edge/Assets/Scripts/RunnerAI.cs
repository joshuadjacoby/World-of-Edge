using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerAI : Enemy {

    // Use this for initialization
    private NavMeshAgent agent;
    private GameObject player;
    private const int DAMAGE = 10;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.destination = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        agent.destination = player.transform.position;
    }

    public override void dealDamage()
    {
        //player.GetComponent<Player>().hurtPlayer(DAMAGE);
        //implement when player script can take damage
    }

}
