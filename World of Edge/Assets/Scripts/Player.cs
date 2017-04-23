using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    private Health health;
    private int edgeCount;
    private int playerLevel;
    private const int EDGES_PER_LEVEL = 10;
    private MeshRenderer playerRenderer;
    void Start () {
        //health = GetComponent<Health>();
        edgeCount = 0;
        playerLevel = 0;
        playerRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
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
    public void flash()
    {
        StartCoroutine(flashPlayer(0.7f));
    }
    public IEnumerator flashPlayer(float duration)
    {
        print("FLASH");
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
