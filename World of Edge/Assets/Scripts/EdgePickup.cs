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
    public float initialDelay;
    public float lifeTime;
	void Start () {
        //the player will handle collisions with edges
        player = GameObject.FindGameObjectWithTag("Player");
        velocity = 0;
        StartCoroutine(deleteAfterSeconds(lifeTime+Random.Range(-.5f,0.5f)));
	}
	
	// Update is called once per frame
	void Update () {
        if(initialDelay > 0)
        {
            initialDelay -= Time.deltaTime;
        } else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < magDistance)
            {
                move();
            }
            else
            {
                velocity = (velocity >= 0) ? velocity - acceleration * Time.deltaTime : 0;
            }
        }
		
	}
    private void move()
    {
        velocity = (velocity < maxSpeed) ? velocity + acceleration * Time.deltaTime : maxSpeed;
        transform.position += (player.transform.position - transform.position).normalized * velocity * Time.deltaTime;
    }
    public IEnumerator deleteAfterSeconds(float seconds)
    {

        while(seconds > 0)
        {
            seconds -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
