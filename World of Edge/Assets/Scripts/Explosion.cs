using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    private Animation animation;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animation>();
        animation["Explosion"].wrapMode = WrapMode.Once;
        animation.Play("Explosion");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
