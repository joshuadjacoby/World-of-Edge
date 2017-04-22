using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Use this for initialization
    private float Frac;
    private Transform playerTransform;
    public float yOffSet;
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Frac = 1f / 10;
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}
    private void move()
    {
        transform.position += (playerTransform.position+Vector3.up*yOffSet - transform.position) * Frac * (1 - Time.deltaTime);

    }
}
