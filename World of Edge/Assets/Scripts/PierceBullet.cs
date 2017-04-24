using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceBullet : BulletParent {

	// Use this for initialization
	void Start () {
        move();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        updateLifeTime();
	}
    void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            health.takeDamage(damage);
            if (enemy != null)
            {
                enemy.Flash();
                if(!health.getInvuln())
                    health.setInvulnPeriod(0.3f);

            }
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
            
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
