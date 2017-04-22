using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : Enemy {

    // Use this for initialization
    private NavMeshAgent agent;
    private GameObject player;
    private Collider playerCollider;
    private const int DAMAGE = 10;
    private float timer;
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        timer = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            hasLOS(player.transform.position);
            timer = 0.2f;
        }
    }

    public override void dealDamage()
    {
        //player.GetComponent<Player>().hurtPlayer(DAMAGE);
        //implement when player script can take damage
    }
    private bool hasLOS(Vector3 dest)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,dest - transform.position, out hit)){
            if (hit.collider.gameObject.CompareTag("Player"))
                return true;
        }
        return false;

    }
}


