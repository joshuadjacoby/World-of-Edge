using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    public float lifetime;
    float currTime;

    void Start() {
        currTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (currTime >= lifetime)
            Destroy(gameObject);
        currTime += Time.deltaTime;
		
	}
}
