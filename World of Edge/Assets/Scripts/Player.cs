using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    private enum gunTypes
    {
        gun0,
        gun1,
        gun2,
        gun3,
        gun4
    }
    private Health health;
    private int edgeCount;
    private int playerLevel;
    private const int TO_LVL_1 = 50;
    private const int TO_LVL_2 = 100;
    private const int TO_LVL_3 = 200;
    private const int TO_LVL_4 = 300;
    private int[] edgeReqs;
    private int gunType;
    private MeshRenderer playerRenderer;
    void Start () {
        //health = GetComponent<Health>();
        edgeCount = 0;
        playerLevel = 0;
        playerRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        edgeReqs = new int[4] {TO_LVL_1,TO_LVL_2,TO_LVL_3,TO_LVL_4};
        gunType = (int)gunTypes.gun0;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void incrementEdgeCount()
    {
        edgeCount++;
        if (edgeCount >= getEdgesToNextLevel())
        {
            edgeCount = 0;
            playerLevel++;
            getNewGun();
        }

    }
    public int getEdgesToNextLevel()
    {
        if(playerLevel >= edgeReqs.Length)
        {
            return 500;
        }
        return edgeReqs[playerLevel];
    }
    public int getGunType()
    {
        return gunType;
    }
    private void getNewGun()
    {
        //to be implemented
    }
    public int getEdgeCount()
    {
        return edgeCount;
    }
    public int getLevel()
    {
        return playerLevel;
    }
    public void flash()
    {
        StartCoroutine(flashPlayer(0.7f));
    }
    public IEnumerator flashPlayer(float duration)
    {
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            playerRenderer.material.SetColor("_Color", Color.Lerp(Color.red,Color.white,elapsedTime/duration));
            yield return null;
        }
        
    }
    void OnCollisionEnter(Collision coll)
    {
        EdgePickup edge = coll.gameObject.GetComponent<EdgePickup>();
        if(edge != null)
        {
            incrementEdgeCount();
            Destroy(coll.gameObject);
        }
    }
}
