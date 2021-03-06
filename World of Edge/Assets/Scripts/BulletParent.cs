﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParent : MonoBehaviour {
    public float lifeTimer;
    public float speed;
    public float damage;
    public Vector3 direction;

    // Update is called once per frame
    public void move()
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
        //transform.position += direction * speed * Time.fixedDeltaTime;
    }
    public void updateLifeTime()
    {
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {

            lifeTimer -= Time.fixedDeltaTime;
        }
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
            Destroy(gameObject);
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
