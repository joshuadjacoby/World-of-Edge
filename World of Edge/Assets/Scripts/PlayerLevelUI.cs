using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLevelUI : MonoBehaviour {

    public Text levelText;
    public string headerName = "LV: ";
    public Player playerObject;
    private int localLevelText;
    // Use this for initialization
    void Start()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (levelText == null)
        {
            levelText = GetComponent<Text>();
        }
        localLevelText = playerObject.GetPlayerLevel();
        levelText.text = headerName + localLevelText;
    }

    // Update is called once per frame
    void Update()
    {
        if (localLevelText != playerObject.GetPlayerLevel())
        {
            levelText.text = headerName + playerObject.GetPlayerLevel();
            localLevelText = playerObject.GetPlayerLevel();
        }
    }
}
