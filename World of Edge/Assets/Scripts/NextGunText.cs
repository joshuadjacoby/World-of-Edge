using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NextGunText : MonoBehaviour {

    // Use this for initialization
    private Player player;
    private Text text;
    public string[] gunNameList;
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //text.text = "NEXT:"+gunNameList[player.getNextGunType()];
	}
}
