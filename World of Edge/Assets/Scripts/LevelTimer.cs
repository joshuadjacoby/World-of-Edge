using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelTimer : MonoBehaviour {

    // Use this for initialization
    public float timeRemaining;
    private Text timerText;
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
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
        }
        
	}
    private string getTime(int time)
    {
        string result = "";
        result += "" + time / 60 + ":";
        result = (time % 60 >= 10) ? result + time % 60 : result + "0" + time % 60;
        return result; 
    }
}
