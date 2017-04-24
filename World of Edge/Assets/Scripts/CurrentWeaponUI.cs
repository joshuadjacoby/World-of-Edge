using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponUI : MonoBehaviour {

    public Text weaponText;
    public Transform scaleTransform;
    public string headerName = "Current Weapon: ";
    public Player playerObject;
    public float shakeDuration;

    private float shakeTimer = 0;
    private string localWeaponName; 
	// Use this for initialization
	void Start () {
        localWeaponName = playerObject.GetWeaponName();
        weaponText.text = headerName + playerObject.GetWeaponName();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (shakeTimer > 0)
        {
            scaleTransform.localScale = new Vector3(1 + shakeTimer / shakeDuration * 0.5f, 1 + shakeTimer / shakeDuration * 0.5f, 1);
            weaponText.color = new Color(1 - shakeTimer / shakeDuration, 1, 1 - shakeTimer / shakeDuration);
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            weaponText.color = new Color(1, 1, 1);
            scaleTransform.localScale = new Vector3(1, 1, 1);
        }
        if (localWeaponName != playerObject.GetWeaponName())
        {
            shakeTimer = shakeDuration;
            weaponText.text = headerName + playerObject.GetWeaponName();
            localWeaponName = playerObject.GetWeaponName();
        }
	}
}
