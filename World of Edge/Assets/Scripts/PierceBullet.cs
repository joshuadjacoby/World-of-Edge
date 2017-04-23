using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceBullet : BulletParent {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        move();
        updateLifeTime();
	}
    void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Flash();
            }
            health.takeDamage((int)damage);
        }
        else
        {
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
