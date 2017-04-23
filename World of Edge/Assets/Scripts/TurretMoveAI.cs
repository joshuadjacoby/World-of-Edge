using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TurretMoveAI : Enemy {

    // Use this for initialization
    private NavMeshAgent agent;
    private List<GameObject> enemyList;
    private GameObject targetEnemy;
    private TurretShootAI turretShootAI;
    private Transform moveTowards;
    void Start () {
        turretShootAI = GetComponentInChildren<TurretShootAI>();
        //turretShootAI.enabled = false;
        //When glued to something, activate turretShootAI
        player = GameObject.FindGameObjectWithTag("Player");
        damage = 10;
        enemyType = (int)enemyTypes.TURRET;
        agent = GetComponent<NavMeshAgent>();
        foreach (TurretBaseAI turretBase in FindObjectsOfType<TurretBaseAI>())
        {
            if (moveTowards == null ||
                Vector3.Distance(transform.position, turretBase.transform.position) < (Vector3.Distance(transform.position, moveTowards.position)))
            {
                moveTowards = turretBase.transform;
            }
        }
        if (moveTowards != null)
        {
            //if there isn't a turret base spawned yet.
            agent.destination = moveTowards.position;

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (moveTowards == null)
        {
            // NOTE : Copy-pasted from shooter finding code in the constructor
            foreach (TurretBaseAI turretBase in FindObjectsOfType<TurretBaseAI>())
            {
                if (Vector3.Distance(transform.position, turretBase.transform.position) < (Vector3.Distance(transform.position, moveTowards.position)))
                {
                    moveTowards = turretBase.transform;
                }

            }

            if (moveTowards != null)
            {
                agent.destination = moveTowards.position;
            }

        }
        else
        {
            agent.destination = moveTowards.position;
        }
        DoUpdate(Time.deltaTime);
    }

    private GameObject getNearestEnemy()
    {
        //still needs to blacklist runners
        int index = 0;
        float min = float.MaxValue;
        for(int i = 0; i < enemyList.Count; i++)
        {
            float distance = (transform.position - enemyList[i].transform.position).magnitude;
            if ( distance < min)
            {
                min = distance;
                index = i;
            }
        }
        return enemyList[index];
    }
}
