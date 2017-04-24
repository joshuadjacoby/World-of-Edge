using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : BulletParent {

    // Use this for initialization
    public float deceleration;
    private Vector3 decelVector;
    private Rigidbody rb;
	void Start () {
        move();
        rb = GetComponent<Rigidbody>();
        decelVector = rb.velocity * -1 * deceleration;
	}
	
	// Update is called once per frame
	void Update () {
        updateLifeTime();
        rb.velocity += decelVector * Time.deltaTime;
	}
}
