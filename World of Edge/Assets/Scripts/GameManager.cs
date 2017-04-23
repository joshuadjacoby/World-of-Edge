using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    // Use this for initialization
    private int edgeCount;
    public Sprite[] playerSprites;
    private GameObject player;
    private Health playerHealth;
    public GameObject[] fadeInObjects;
    private int playerLevel;
    private bool isDead;
    private const int EDGES_PER_LEVEL = 10;
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        isDead = false;
        for(int i = 0; i < fadeInObjects.Length; i++)
        {
            fadeInObjects[i].SetActive(false);
        }
        playerLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            takeDamage(10);
        }
	}
    public void incrementEdgeCount()
    {
        edgeCount++;
        if(edgeCount >= EDGES_PER_LEVEL)
        {
            playerLevel++;
        }
    }
    public int getEdgeCount()
    {
        return edgeCount;
    }
    public Sprite getPlayerSprite()
    {
        return playerSprites[playerLevel];
    }
    private bool playerIsDead()
    {
        return playerHealth.health <= 0;
    }
    public void killPlayer()
    {
        player.SetActive(false);
        StartCoroutine(gameOverScreen());
    }
    public void takeDamage(int damage)
    {
        playerHealth.health -= damage;
        if (playerIsDead() && !isDead)
        {
            //above statement makes this run only once
            //Only when the player transitions from not-dead to dead
            killPlayer();
        }
    }
    public IEnumerator gameOverScreen()
    {
        Color[] originalColor = new Color[fadeInObjects.Length];
        for(int i = 0; i < fadeInObjects.Length; i++)
        {
            fadeInObjects[i].SetActive(true);
        }
        originalColor[0] = fadeInObjects[0].GetComponent<Image>().color;
        originalColor[1] = fadeInObjects[1].GetComponent<Text>().color;
        originalColor[2] = fadeInObjects[2].GetComponent<Text>().color;
        Color transparent = new Color(0, 0, 0, 0);


        float elapsedTime = 0;
        float duration = 1f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
           
            fadeInObjects[0].GetComponent<Image>().color = Color.Lerp(transparent, originalColor[0], elapsedTime / duration);
            fadeInObjects[1].GetComponent<Text>().color = Color.Lerp(transparent, originalColor[1], elapsedTime / duration);
            fadeInObjects[2].GetComponent<Text>().color = Color.Lerp(transparent, originalColor[2], elapsedTime / duration);
            yield return null;
        }
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
            yield return null;
        }
    }


}
