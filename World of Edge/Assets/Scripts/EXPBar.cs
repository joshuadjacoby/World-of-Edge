using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBar : MonoBehaviour {

    // Use this for initialization
    private RectTransform expBarInside;
    private RectTransform expBarOutside;
    private Player player;
	void Start () {
        expBarInside = GetComponent<RectTransform>();
        expBarOutside = GetComponentInParent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        expBarInside.anchorMax = new Vector2((float)player.getEdgeCount() / player.getEdgesToNextLevel(), 1);
	}
}
