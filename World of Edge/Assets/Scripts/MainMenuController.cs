using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour {

    // Use this for initialization
    public UIRevealer[] nameList;
    public UIRevealer[] roleList;
    public EventSystem eventSystem;
    public GameObject[] buttonList;
    public UIRevealer creditsRevealer;
    private const float INITIALDELAY = 0.2f;
    private const float REPEATDELAY = 0.15f;
    private IEnumerator creditsCoroutine;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playPress()
    {
        SceneManager.LoadScene("Main");
    }
    public void creditsPress()
    {
        eventSystem.SetSelectedGameObject(buttonList[3]);
        creditsRevealer.revealUI();
        creditsCoroutine = creditsReveal();
        StartCoroutine(creditsCoroutine);
    }
    public void quitPress()
    {
        Application.Quit();
    }
    public void backPress()
    {
        eventSystem.SetSelectedGameObject(buttonList[0]);
        creditsRevealer.hideUI();
        StopCoroutine(creditsCoroutine);
        for(int i = 0; i < nameList.Length; i++)
        {
            nameList[i].hideUI();
            roleList[i].hideUI();
        }
    }
    public IEnumerator creditsReveal()
    {
        yield return new WaitForSeconds(INITIALDELAY);
        for(int i = 0; i < nameList.Length; i++)
        {
            nameList[i].revealUI();
            roleList[i].revealUI();
            yield return new WaitForSeconds(REPEATDELAY);
        }
    }
}
