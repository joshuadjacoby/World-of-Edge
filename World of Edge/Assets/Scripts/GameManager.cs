using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    // Use this for initialization
    public Sprite[] playerSprites;
    private GameObject player;
    private Health playerHealth;
    public GameObject[] fadeInObjects;
    public Text edgeText;
    private bool isDead;
    private EnemyManager[] enemyManagers;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        isDead = false;
        for(int i = 0; i < fadeInObjects.Length; i++)
        {
            fadeInObjects[i].SetActive(false);
        }

        enemyManagers = FindObjectsOfType<EnemyManager>();

	}
	
	// Update is called once per frame
	void Update () {
        edgeText.text = "EDGE:"+player.GetComponent<Player>().getEdgeCount();
        if (playerIsDead()){
            killPlayer();
        }
	}
    
    public Sprite getPlayerSprite()
    {
        return playerSprites[player.GetComponent<Player>().getLevel()];
    }
    private bool playerIsDead()
    {
        return playerHealth.currentHealth <= 0;
    }
    public void killPlayer()
    {
        player.SetActive(false);

        // stops all enemy spawners from spawning stuff when player is dead
        for (int i = 0; i < enemyManagers.Length; i++)
        {
            enemyManagers[i].playerIsDead(true);
        }

        if (!isDead)
        {
            StartCoroutine(gameOverScreen());
        }
    }
    
    public IEnumerator gameOverScreen()
    {
        isDead = true;
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
