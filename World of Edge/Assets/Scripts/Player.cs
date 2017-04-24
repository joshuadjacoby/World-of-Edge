using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Use this for initialization

    public Health health;
    public Shooter[] shooter;
    public int edgeCount = 0;
    public int playerLevel = 0;
    public int[] edgesRequiredForLevels = { 50, 100, 200, 300 };
    public int currentEquippedShooter;
    public SpriteRenderer spriteRenderer;

    public void incrementEdgeCount()
    {
        if (playerLevel < edgesRequiredForLevels.Length - 1)
        {
            edgeCount++;
            if (edgeCount >= edgesRequiredForLevels[playerLevel])
            {
                edgeCount = 0;
                ++playerLevel;
                ++currentEquippedShooter;
            }
        }
    }

    public float GetNextLevelProgress()
    {
        if (playerLevel < edgesRequiredForLevels.Length)
        {
            return edgeCount / edgesRequiredForLevels[playerLevel];
        }
        return 1;
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
            if (elapsedTime % (flashSpeed * 2) > flashSpeed)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            yield return null;
        }
        spriteRenderer.enabled = true;
    }

    void OnCollisionEnter(Collision coll)
    {
        EdgePickup edge = coll.gameObject.GetComponent<EdgePickup>();
        if (edge != null)
        {
            incrementEdgeCount();
            Destroy(coll.gameObject);
        }
    }
}
