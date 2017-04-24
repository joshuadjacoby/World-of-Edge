using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour {

    // Use this for initialization
    public Texture2D texture;
    public Vector2 offSet;
	void Start () {
        Cursor.SetCursor(texture, offSet, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.SetCursor(texture, offSet, CursorMode.Auto);
    }
}
