using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float currentHealth, maxHealth;
    public int healthBarWidth = 60;
    public int offset = 8;
    public int healthBarHeight = 8;
    private Image healthBar;
    private Image backBar;
    private RectTransform canvas;
    private bool hpBarCreated = false;
    private GameObject backGO, healthGO;
    private bool isInvuln;
    private const float INVULN_PERIOD = 1.5f;
    public Color backColor = new Color(0.2f, 0.0f, 0.0f, 0.5f);
    public Color innerColor = new Color(255f, 0.0f, 0.0f, 0.5f);
    private void Start()
    {

        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        if (gameObject.tag == "Player")
        {
            spawnHPBar();
            hpBarCreated = true;
        }
        isInvuln = false;

    }

    private float curbw = 100.0f;
    private void setBars()
    {
        if (!healthBar || !backBar)
        {
            return;
        }
        Vector3 sptd = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 screenPoint = new Vector2(sptd.x, sptd.y);
        float h = Camera.main.pixelHeight;
        float w = Camera.main.pixelWidth;
        screenPoint.x *= w;
        screenPoint.y *= h;
        screenPoint.x -= w / 2.0f;
        screenPoint.y -= h / 2.0f;

        //screenPoint.x -= 50f;
        screenPoint.x -= healthBarWidth / 2;
        screenPoint.y += 25f;

        healthBar.rectTransform.anchoredPosition = screenPoint;
        backBar.rectTransform.anchoredPosition = screenPoint;

        curbw = Mathf.Lerp(curbw, currentHealth / maxHealth * 100.0f, Time.deltaTime * 8.0f);
        healthBar.rectTransform.sizeDelta = new Vector2(curbw/100*healthBarWidth, healthBarHeight);



    }

    private void Update()
    {
        if (!hpBarCreated && currentHealth < maxHealth)
        {
            spawnHPBar();
            hpBarCreated = true;
        }
        else if (hpBarCreated)
        {
            setBars();
        }
        //if (currentHealth <= 0)
        //{
        //    Destroy(healthBar);
        //    Destroy(backBar);
        //}
    }

    private void spawnHPBar()
    {
        backGO = new GameObject("healthbarback");
        backGO.transform.parent = canvas.Find("HealthBars").transform;
        healthGO = new GameObject("healthbar");
        healthGO.transform.parent = canvas.Find("HealthBars").transform;

        healthBar = healthGO.AddComponent<Image>();
        backBar = backGO.AddComponent<Image>();

        //healthBar.color = new Color(255f, 0.0f, 0.0f, 0.5f);
        //backBar.color = new Color(0.2f, 0.0f, 0.0f, 0.5f);
        healthBar.color = innerColor;
        backBar.color = backColor;

        healthBar.rectTransform.sizeDelta = new Vector2(healthBarWidth, healthBarHeight);
        backBar.rectTransform.sizeDelta = new Vector2(healthBarWidth, healthBarHeight);

        backBar.rectTransform.pivot = Vector2.zero;
        healthBar.rectTransform.pivot = Vector2.zero;
    }


    private void OnDestroy()
    {
        Destroy(healthBar);
        Destroy(backBar);
    }
    public void takeDamage(int damage)
    {
        if (!getInvuln())
        {
            currentHealth -= damage;
            if(gameObject.GetComponent<Player>() != null)
            {
                setInvulnPeriod(INVULN_PERIOD);
                gameObject.GetComponent<Player>().flashInvuln(INVULN_PERIOD);
            }
        }
        
        
    }
    public void setInvuln(bool x)
    {
        isInvuln = x;
    }
    public bool getInvuln()
    {
        return isInvuln;
    }
    public void setInvulnPeriod(float duration)
    {
        StartCoroutine(invulnPeriod(duration));
    }
    public IEnumerator invulnPeriod(float duration)
    {
        setInvuln(true);
        yield return new WaitForSeconds(duration);
        setInvuln(false);
    }

}
