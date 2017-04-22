using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection.normalized;

        gameObject.GetComponent<Rigidbody>().AddForce(shootDirection * 1000);
       
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!gameObject.GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}
