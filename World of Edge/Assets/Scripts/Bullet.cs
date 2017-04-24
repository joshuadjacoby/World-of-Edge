using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletParent
{

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
