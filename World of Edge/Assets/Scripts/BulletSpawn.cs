using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Candy"), gameObject.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
