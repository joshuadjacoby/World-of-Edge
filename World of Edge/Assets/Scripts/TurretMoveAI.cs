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
	void Start () {
        turretShootAI = GetComponentInChildren<TurretShootAI>();
        //turretShootAI.enabled = false;
        //When glued to something, activate turretShootAI
        player = GameObject.FindGameObjectWithTag("Player");
        health = 100;
        damage = 10;
        enemyType = (int)enemyTypes.TURRET;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
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
