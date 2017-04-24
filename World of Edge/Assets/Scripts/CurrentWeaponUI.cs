using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponUI : MonoBehaviour {

    public Text weaponText;
    public string headerName = "Current Weapon: ";
    public Player playerObject;
    private string localWeaponName; 
	// Use this for initialization
	void Start () {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (weaponText == null)
        {
            weaponText = GetComponent<Text>();
        }
        localWeaponName = playerObject.GetWeaponName();
        weaponText.text = headerName + playerObject.GetWeaponName();
	}
	
	// Update is called once per frame
	void Update () {
		if (localWeaponName != playerObject.GetWeaponName())
        {
            weaponText.text = headerName + playerObject.GetWeaponName();
            localWeaponName = playerObject.GetWeaponName();
        }
	}
}
