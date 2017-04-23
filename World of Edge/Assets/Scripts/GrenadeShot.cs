using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeShot : MonoBehaviour {
    public float lifeTimer;
    public float speed;
    public float damage;
    public Vector3 direction;

    public float blastRadius;

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider hit in hitColliders) {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Flash();
                }
                health.takeDamage((int)damage);
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
}
