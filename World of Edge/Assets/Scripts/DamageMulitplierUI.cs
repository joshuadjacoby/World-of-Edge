using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMulitplierUI : MonoBehaviour {

    public Text damageMuliplierText;
    //public string headerName = "Current Weapon: ";
    public string multiplierString = "x";
    public Player playerObject;
    private float localDamageMultiplier;

    // Use this for initialization
    void Start () {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if (damageMuliplierText == null)
        {
            damageMuliplierText = GetComponent<Text>();
        }

        localDamageMultiplier = playerObject.GetDamageMultiplier();
        damageMuliplierText.text = localDamageMultiplier + multiplierString;

    }
	
	// Update is called once per frame
	void Update () {
		if (playerObject.GetDamageMultiplier() != localDamageMultiplier)
        {
            damageMuliplierText.text = playerObject.GetDamageMultiplier() + multiplierString;
            localDamageMultiplier = playerObject.GetDamageMultiplier();
        }
	}
}
