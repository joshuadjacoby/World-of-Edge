using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    
    public GameObject player;
    public Health playerHealth;
    public GameObject[] fadeInObjects;
    public GameObject[] winFadeInObjects;
    public EnemyManager[] enemyManagers;
    public AudioSource gameOverSound;
    public float gameOverFadeInDuration;
    public float spawnTimeReduction;
    public int spawnTimeReductionThreshold;

    private bool gameIsOver;
    private float gameOverFadeInTimer;
    private Color[] originalColors;
    private Color[] winOriginalColors;
    private bool won;

    void Start ()
    {
        gameIsOver = false;
        gameOverFadeInTimer = 0;

        originalColors = new Color[fadeInObjects.Length];
        for (int fadeInObjectIndex = 0; fadeInObjectIndex < fadeInObjects.Length; ++fadeInObjectIndex)
        {
            fadeInObjects[fadeInObjectIndex].SetActive(false);
            Image image = fadeInObjects[fadeInObjectIndex].GetComponent<Image>();
            Text text = fadeInObjects[fadeInObjectIndex].GetComponent<Text>();
            if (image != null)
            {
                originalColors[fadeInObjectIndex] = image.color;
            }
            if (text != null)
            {
                originalColors[fadeInObjectIndex] = text.color;
            }
        }
        winOriginalColors = new Color[winFadeInObjects.Length];
        for (int fadeInObjectIndex = 0; fadeInObjectIndex < winFadeInObjects.Length; ++fadeInObjectIndex)
        {
            winFadeInObjects[fadeInObjectIndex].SetActive(false);
            Image image = winFadeInObjects[fadeInObjectIndex].GetComponent<Image>();
            Text text = winFadeInObjects[fadeInObjectIndex].GetComponent<Text>();
            if (image != null)
            {
                winOriginalColors[fadeInObjectIndex] = image.color;
            }
            if (text != null)
            {
                winOriginalColors[fadeInObjectIndex] = text.color;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerHealth.currentHealth <= 0)
        {
            player.SetActive(false);
            if (!gameIsOver)
            {
                gameIsOver = true;
                if (!won)
                {
                    gameOverSound.Play();
                    foreach (EnemyManager enemyManager in enemyManagers)
                    {
                        enemyManager.playerDead = true;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GetComponent<AudioSource>().Stop();
                    SceneManager.LoadScene("Main");
                    return;
                }
                if (gameOverFadeInTimer < gameOverFadeInDuration)
                {
                    gameOverFadeInTimer += Time.deltaTime;

                    for (int fadeInObjectIndex = 0; fadeInObjectIndex < fadeInObjects.Length; ++fadeInObjectIndex)
                    {
                        fadeInObjects[fadeInObjectIndex].SetActive(true);

                        Image image = fadeInObjects[fadeInObjectIndex].GetComponent<Image>();
                        Text text = fadeInObjects[fadeInObjectIndex].GetComponent<Text>();
                        if (image != null)
                        {
                            image.color = Color.Lerp(new Color(0, 0, 0, 0), originalColors[fadeInObjectIndex], gameOverFadeInTimer / gameOverFadeInDuration);
                        }
                        if (text != null)
                        {
                            text.color = Color.Lerp(new Color(0, 0, 0, 0), originalColors[fadeInObjectIndex], gameOverFadeInTimer / gameOverFadeInDuration);
                        }
                    }
                }
            }
        }
        else
        {
            won = true;
            foreach (EnemyManager enemyManager in enemyManagers)
            {
                if (enemyManager.enemyList.Count < spawnTimeReductionThreshold)
                {
                    enemyManager.spawnTimeReduction = spawnTimeReduction * (1f - ((float)enemyManager.enemyList.Count / (float)spawnTimeReductionThreshold));
                }
                else if (enemyManager.spawnTimeReduction != 0)
                {
                    enemyManager.spawnTimeReduction = 0;
                }
                if (enemyManager.EnemiesLeftToKill() > 0)
                {
                    won = false;
                    break;
                }
            }
            if (won)
            {
                fadeInObjects = winFadeInObjects;
                originalColors = winOriginalColors;
                playerHealth.currentHealth = 0;
            }
        }
	}
}
