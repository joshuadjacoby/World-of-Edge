using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTestScript : MonoBehaviour {

    GameObject target;
    GameObject[] targetList;
    // Use this for initialization
    void Start () {
        targetList = GameObject.FindGameObjectsWithTag("Enemy");
        target = targetList[0].gameObject == gameObject ? targetList[1] : targetList[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent == null)
        {
            transform.position =Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 5.0f);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent== null && collision.transform.tag == "Enemy")
        {
            collision.transform.parent= transform;
            collision.gameObject.tag = "EnemyComponent";
            if (collision.gameObject == target)
            {
                changeTargets();
            }
        }

    }

    private void changeTargets()
    {
        targetList = GameObject.FindGameObjectsWithTag("Enemy");
        target = targetList[0].gameObject == gameObject ? targetList[1] : targetList[0];
    }
}
