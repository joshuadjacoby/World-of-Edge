using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTestScript : MonoBehaviour {

    GameObject target;
    GameObject[] targetList;
    // Use this for initialization
    void Start () {
        changeTargets();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.parent == null && targetList.Length != 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 5.0f);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent == null)
        {
            if (collision.transform.childCount > 0)
            {
                //unNestChildren(collision.transform);
            }
            collision.transform.parent = transform;
            collision.gameObject.tag = "EnemyComponent";
            if (collision.gameObject == target)
            {
                changeTargets();
            }
        }
    }

    private void unNestChildren(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.childCount > 0)
            {
                unNestChildren(child);
            }
            child.parent = transform;
            child.gameObject.tag = "EnemyComponent";
            if (child.gameObject == target)
            {
                changeTargets();
            }
        }
    }

    private void changeTargets()
    {
        targetList = GameObject.FindGameObjectsWithTag("Enemy");
        if (targetList.Length == 1)
        {
            return;
        }
        do
        {
            target = targetList[Random.Range(0, targetList.Length)];
        } while (target == gameObject);
    }
}
