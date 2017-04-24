using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLevelUI : MonoBehaviour {

    public Text levelText;
    public Transform scaleTransform;
    public string headerName = "LV: ";
    public Player playerObject;
    public float shakeDuration;

    private float shakeTimer = 0;
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
        if (shakeTimer > 0)
        {
            scaleTransform.localScale = new Vector3(1 + shakeTimer / shakeDuration * 0.5f, 1 + shakeTimer / shakeDuration * 0.5f, 1);
            levelText.color = new Color(1 - shakeTimer / shakeDuration, 1, 1 - shakeTimer / shakeDuration);
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            levelText.color = new Color(1, 1, 1);
            scaleTransform.localScale = new Vector3(1, 1, 1);
        }
        if (localLevelText != playerObject.GetPlayerLevel())
        {
            shakeTimer = shakeDuration;
            levelText.text = headerName + playerObject.GetPlayerLevel();
            localLevelText = playerObject.GetPlayerLevel();
        }
    }
}
