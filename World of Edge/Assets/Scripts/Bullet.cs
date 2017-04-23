using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTimer;
    public float speed;
    public float damage;
    public Vector3 direction;

    // Update is called once per frame
    void FixedUpdate () {
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
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
            health.currentHealth -= damage;

            Destroy(gameObject);
        }
    }
}
