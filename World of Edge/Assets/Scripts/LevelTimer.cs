using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelTimer : MonoBehaviour {

    // Use this for initialization
    public EnemyManager[] enemyManagers;
    //public float timeRemaining;
    public Text timerText;
    public float shakeDuration;

    private float shakeTimer = 0;
    private int storeCount = 0;

	// Update is called once per frame
	void Update () {
        if (shakeTimer > 0)
        {
            timerText.rectTransform.localScale = new Vector3(1 + shakeTimer / shakeDuration * 0.5f, 1 + shakeTimer / shakeDuration * 0.5f, 1);
            timerText.color = new Color(1 - shakeTimer / shakeDuration, 1 - shakeTimer / shakeDuration, 1);
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            timerText.color = new Color(1, 1 , 1);
            timerText.rectTransform.localScale = new Vector3(1, 1, 1);
        }
        int enemiesRemaining = 0;
        foreach (EnemyManager enemyManager in enemyManagers)
        {
            enemiesRemaining += enemyManager.EnemiesLeftToKill();
        }
        timerText.text = enemiesRemaining.ToString();
        if (storeCount != enemiesRemaining)
        {
            shakeTimer = shakeDuration;
        }
        storeCount = enemiesRemaining;
        /*
        if(timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining < 6)
            {
                timerText.color = Color.red;
            }
            timerText.text = getTime((int)timeRemaining);
        } else
        {
            timerText.text = "0:00";
        }*/

    }
    /*
    private string getTime(int time)
    {
        string result = "";
        result += "" + time / 60 + ":";
        result = (time % 60 >= 10) ? result + time % 60 : result + "0" + time % 60;
        return result; 
    }*/
}
