using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour {

    // Use this for initialization
    public int health;
    private bool AIActive;
    private GameObject player;


    private const float PARTICLE_LIFETIME = 2f;
	void Start () {
        AIActive = true;
        player = GameObject.FindGameObjectWithTag("player");
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
    public abstract void dealDamage(int damage);

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
    void OnCollisionEnter2D(Collision2D coll)
    {

    }


}
