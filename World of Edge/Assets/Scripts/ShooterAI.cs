using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : Enemy {

    // Use this for initialization
    public float shootRadius;
    public float fireDelay;
    private float fireTimer;
    private NavMeshAgent agent;
    private Collider playerCollider;
    private bool LOS;
    private const float LOSREPEAT = 0.5f;
    //get generic shoot script
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
        damage = 10;
        LOS = false;
        fireTimer = 0;
        StartCoroutine(LOSLoop());
        enemyType = (int)enemyTypes.SHOOTER;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(LOS && (agent.transform.position-transform.position).magnitude < shootRadius)
        {
            agent.destination = transform.position;
            aimAt(player.transform.position);
            if(fireTimer < fireDelay)
            {
                fireTimer += Time.deltaTime;
            } else
            {
                fireAt(player.transform.position);
                fireTimer = 0;
            }
        }  else
        {
            fireTimer = 0;
            agent.destination = player.transform.position;
        }
    }
    private IEnumerator LOSLoop()
    {
        LOS = hasLOS(player.transform.position);
        yield return new WaitForSeconds(LOSREPEAT);
        StartCoroutine(LOSLoop());
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
    private void fireAt(Vector3 target)
    {
    }
    private void aimAt(Vector3 target)
    {
        Vector3 aimVector = target - transform.position;
        float step = 3*Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, aimVector, step, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}


