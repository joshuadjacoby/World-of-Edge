using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour {

    // Use this for initialization
    private RectTransform expBarInside;
    private RectTransform expBarOutside;
    public Text weaponText;
    public string[] weaponNames;
    public string stringBeforeGunName;
    private Player player;
	void Start () {
        expBarInside = GetComponent<RectTransform>();
        expBarOutside = GetComponentInParent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        expBarInside.anchorMax = new Vector2((float)player.getEdgeCount() / player.getEdgesToNextLevel(), 1);
        weaponText.text = stringBeforeGunName + weaponNames[player.getNextGunType()];
	}
}
