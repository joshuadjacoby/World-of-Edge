﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour {

    // Use this for initialization
    public enum enemyTypes
    {
        RUNNERAI,
        SHOOTER,
        TURRET
    }
    public int health;
    private bool AIActive = true;
    protected GameObject player;
    protected int damage;
    protected int enemyType;
    private const float PARTICLE_LIFETIME = 2f;
	void Start () {
        
	}
	void Update()
    {
    }
	// Update is called once per frame
    public void spawn(int x, int y)
    {
        transform.position = new Vector3(x, y);
    }
    public void setAI(bool x)
    {
        AIActive = x;
    }
    
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.kill();
        }
    }
    public void dealDamage()
    {
        //player.getComponent<Player>().takeDamage(damage);
    }

    public void kill()
    {
        deglue();
        destroyAfterDelay(PARTICLE_LIFETIME);
    }
    public IEnumerator destroyAfterDelay(float duration)
    {
        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    private void glue()
    {

    }
    private void deglue()
    {

    }
    void OnCollisionEnter(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            dealDamage();
        }
        if (coll.gameObject.CompareTag("Enemy"))
        {
            //to be implemented
        }
    }


}
