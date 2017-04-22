using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTimer;
    public float speed;
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
        }
    }
}
