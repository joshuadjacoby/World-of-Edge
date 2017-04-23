using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    private Health health;
    private int edgeCount;
    private int playerLevel;
    private const int EDGES_PER_LEVEL = 10;

    void Start () {
        //health = GetComponent<Health>();
        edgeCount = 0;
        playerLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void incrementEdgeCount()
    {
        edgeCount++;
        if (edgeCount >= EDGES_PER_LEVEL)
        {
            playerLevel++;
        }
    }
    public int getEdgeCount()
    {
        return edgeCount;
    }
    public int getLevel()
    {
        return playerLevel;
    }
    
    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("COLLISION");
        EdgePickup edge = coll.gameObject.GetComponent<EdgePickup>();
        if(edge != null)
        {
            incrementEdgeCount();
            Destroy(coll.gameObject);
        }
    }
}
