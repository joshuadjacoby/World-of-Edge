using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BulletParent {

    // Use this for initialization
    public float deceleration;
    public float blastRadius;
    public GameObject explosion;
    public LayerMask hitLayer;
    public float fuseDuration;
    void Start () {
        move();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody>().velocity *= deceleration * (1 - Time.deltaTime);
        if(fuseDuration > 0)
        {
            fuseDuration -= Time.deltaTime;
        } else
        {
            explode();
        }
    }
    void explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius, hitLayer);
        foreach (Collider hit in hitColliders)
        {
            Health health = hit.gameObject.GetComponent<Health>();
            if (health != null)
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Flash();
                }
                health.takeDamage((int)damage);
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody>().velocity = Vector3.Reflect(transform.forward, collision.contacts[0].normal)*GetComponent<Rigidbody>().velocity.magnitude;
            direction = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
