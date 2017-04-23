using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    
    private Health health;
    private int edgeCount;
    private int playerLevel;
    private const int TO_LVL_1 = 50;
    private const int TO_LVL_2 = 100;
    private const int TO_LVL_3 = 200;
    private const int TO_LVL_4 = 300;
    private const int HIGH_VELOCITY = 0;
    private const int PIERCING = 1;
    private const int RICOCHET = 2;
    private const int EXPLOSIVE = 3;
    private const int SPREAD = 4;
    private int[] edgeReqs;
    private int gun;
    private int nextGun;
    private MeshRenderer playerRenderer;
    void Start () {
        //health = GetComponent<Health>();
        edgeCount = 0;
        playerLevel = 0;
        playerRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        edgeReqs = new int[4] {TO_LVL_1,TO_LVL_2,TO_LVL_3,TO_LVL_4};
        gun = HIGH_VELOCITY;
        while (true)
        {
            nextGun = (int)Random.Range(0, 4.9999f);
            if (nextGun != gun)
            {
                break;
            }
        }
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
        return gun;
    }
    public int getNextGunType()
    {
        return nextGun;
    }
    private void getNewGun()
    {
        gun = nextGun;
        while (true)
        {
            nextGun = (int)Random.Range(0, 4.9999f);
            if(nextGun != gun)
            {
                break;
            }
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
    /*
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
    */
    public void flashInvuln(float duration)
    {
        StartCoroutine(flashPlayerInvuln(duration));
    }
    public IEnumerator flashPlayerInvuln(float duration)
    {
        float flashSpeed = 0.1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime % (flashSpeed*2) > flashSpeed)
            {
                playerRenderer.enabled = false;
            } else
            {
                playerRenderer.enabled = true;
            }
            yield return null;
        }
        playerRenderer.enabled = true;
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
