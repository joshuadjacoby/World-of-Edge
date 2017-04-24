using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRicochetBullet : BulletParent {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
            health.takeDamage(damage);
            direction = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            if (collision.gameObject.tag == "Wall")
            {
                direction = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
