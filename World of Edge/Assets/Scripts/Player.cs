using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{

    // Use this for initialization

    public KeyCode switchWeapon;
    public Health health;
    public Shooter[] shooters;
    public int edgeCount = 0;
    public int playerLevel = 0;
    public int equippedWeaponIndex = 0;
    public int[] edgesRequiredForLevels = { 50, 100, 200, 300 };
    public string[] animations = { "Square", "Pentagon", "Hexagon", "Septagon" };
    public string[] weaponNames = { "Rapid Fire", "Shotgun", "Cow", "Sine Gun", "Super Ricochet" };
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        foreach (Shooter shooter in shooters)
        {
            shooter.enabled = false;
        }
        if (shooters.Length > 0)
        {
            shooters[0].enabled = true;
        }
    }

    public void incrementEdgeCount()
    {
        if (playerLevel < edgesRequiredForLevels.Length - 1)
        {
            edgeCount++;
            if (edgeCount >= edgesRequiredForLevels[playerLevel])
            {
                edgeCount = 0;

                foreach (Shooter shooter in shooters)
                {
                    shooter.enabled = false;
                }

                ++playerLevel;
                equippedWeaponIndex = playerLevel;
                health.currentHealth = health.maxHealth;

                foreach (Shooter shooter in shooters)
                {
                    shooter.damageMultiplier = 1 + playerLevel * 0.1f;
                }

                if (equippedWeaponIndex < shooters.Length)
                {
                    shooters[equippedWeaponIndex].enabled = true;
                }
                else
                {
                    equippedWeaponIndex = shooters.Length - 1;
                    shooters[equippedWeaponIndex].enabled = true;
                }

                if (playerLevel < animations.Length)
                {
                    animator.Play(animations[playerLevel]);
                }
                else
                {
                    animator.Play(animations.Length - 1);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(switchWeapon))
        {
            equippedWeaponIndex = equippedWeaponIndex + 1;
            if (equippedWeaponIndex > playerLevel)
            {
                equippedWeaponIndex = 0;
            }
            foreach (Shooter shooter in shooters)
            {
                shooter.enabled = false;
            }
            shooters[equippedWeaponIndex].enabled = true;
        }
    }

    public string GetWeaponName()
    {
        if (equippedWeaponIndex < weaponNames.Length)
        {
            return weaponNames[equippedWeaponIndex];
        }
        return "";
    }

    public float GetDamageMultiplier()
    {
        if (equippedWeaponIndex < shooters.Length)
        {
            return shooters[equippedWeaponIndex].damageMultiplier;
        }
        return 1;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public float GetNextLevelProgress()
    {
        if (playerLevel < edgesRequiredForLevels.Length)
        {
            return (float)edgeCount / (float)edgesRequiredForLevels[playerLevel];
        }
        return 1;
    }

    /*
    public void flash()
    {
        StartCoroutine(flashPlayer(0.7f));
    }
    public IEnumerator flashPlayer(float duration)
    {
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            playerRenderer.material.SetColor("_Color", Color.Lerp(Color.red,Color.white,elapsedTime/duration));
            yield return null;
        }
        
    }
    */
    public void flashInvuln(float duration)
    {
        StartCoroutine(flashPlayerInvuln(duration));
    }
    public IEnumerator flashPlayerInvuln(float duration)
    {
        float flashSpeed = 0.1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime % (flashSpeed * 2) > flashSpeed)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            yield return null;
        }
        spriteRenderer.enabled = true;
    }

    void OnCollisionEnter(Collision coll)
    {
        EdgePickup edge = coll.gameObject.GetComponent<EdgePickup>();
        if (edge != null)
        {
            incrementEdgeCount();
            Destroy(coll.gameObject);
        }
    }
}
