using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeShot : BulletParent {
    public float blastRadius;
    public GameObject explosion;
    public LayerMask hitLayer;

    void FixedUpdate() {
        move();
        updateLifeTime();
    }

    void OnCollisionEnter(Collision collision) 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius, hitLayer);
        foreach(Collider hit in hitColliders) {
            Health health = hit.gameObject.GetComponent<Health>();
            if (health != null)
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Flash();
                }
                health.takeDamage((int)damage);
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
