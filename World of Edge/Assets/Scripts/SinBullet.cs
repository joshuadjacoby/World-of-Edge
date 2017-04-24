using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinBullet : BulletParent {

    // Use this for initialization
    private float orthoVelocity;
    public float oscilliationSpeed;
    public float oscillationMagnitude;
    private float timer;
    public bool flipX = false;
	void Start () {
        orthoVelocity = 0;
        timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        timer += oscilliationSpeed * Time.deltaTime;
        
        if (flipX)
        {
            orthoVelocity = -oscillationMagnitude * Mathf.Cos(timer);
        } else
        {
            orthoVelocity = oscillationMagnitude * Mathf.Cos(timer);
        }
        
        GetComponent<Rigidbody>().velocity = direction * speed + orthoVelocity*transform.right;
        updateLifeTime();
        
	}
}
