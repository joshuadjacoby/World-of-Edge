using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgePickup : MonoBehaviour {

    // Use this for initialization
    private GameObject player;
    public float magDistance;
    public float maxSpeed;
    private float velocity;
    public float acceleration;
	void Start () {
        //the player will handle collisions with edges
        player = GameObject.FindGameObjectWithTag("Player");
        velocity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position,player.transform.position) < magDistance)
        {
            move();
        } else
        {
            velocity = (velocity >= 0) ? velocity - acceleration * Time.deltaTime : 0;
        }
	}
    private void move()
    {
        velocity = (velocity < maxSpeed) ? velocity + acceleration * Time.deltaTime : maxSpeed;
        transform.position += (player.transform.position - transform.position).normalized * velocity * Time.deltaTime;
    }
}
