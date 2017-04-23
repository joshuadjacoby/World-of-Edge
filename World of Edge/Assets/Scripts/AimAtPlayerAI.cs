using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayerAI : Enemy
{
    Transform targetTransform;

	// Use this for initialization
	void Start ()
    {
        PlayerMovement playerMove = FindObjectOfType<PlayerMovement>();
        if (playerMove != null)
        {
            targetTransform = playerMove.transform;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - targetTransform.position, Vector3.up);
    }
}
