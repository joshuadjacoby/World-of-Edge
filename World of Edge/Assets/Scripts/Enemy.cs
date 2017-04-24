using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    // Use this for initialization
    public enum enemyTypes
    {
        RUNNERAI,
        SHOOTER,
        TURRET
    }

    public Health health;
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.15f;
    public EnemyManager enemyManager;
    public int collisionDamage;
    private bool AIActive = true;
    protected Player player;
    protected int enemyType;
    public float deathDelay;
    public int edgesToSpawn;
    public GameObject edgePrefab;
    private float flashTimer;

    public void DoStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void DoUpdate(float deltaTime)
    {
        if (spriteRenderer != null)
        {
            if (flashTimer > 0)
            {
                float colorDiff = 1 - flashTimer / flashDuration;
                spriteRenderer.color = new Color(1, colorDiff, colorDiff);
                flashTimer -= deltaTime;
            }
            else
            {
                spriteRenderer.color = Color.white;
                flashTimer = 0;
            }
        }
        if (health.currentHealth <= 0)
        {
            //deglue();
            //StartCoroutine(destroyAfterDelay(deathDelay));
            enemyManager.enemyList.Remove(gameObject);
            ++player.combo;
            edgeExplosion(edgesToSpawn);
            Destroy(gameObject);
        }
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
    public void edgeExplosion(int numEdges)
    {
        for(int i = 0; i < numEdges; i++)
        {
            float rngAngle = Random.Range(0, 360);
            float rngSpeed = Random.Range(3, 10);
            Vector3 rngVector = new Vector3(Mathf.Cos(rngAngle), 0, Mathf.Sin(rngAngle));
            Debug.DrawRay(transform.position, rngVector, Color.green, 2f);
            GameObject edge = Instantiate(edgePrefab) as GameObject;
            edge.GetComponent<Rigidbody>().velocity = rngVector*rngSpeed;
            edge.transform.position = transform.position+rngVector;
            
        }
    }
    public IEnumerator destroyAfterDelay(float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
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

    public void Flash()
    {
        spriteRenderer.color = Color.red;
        flashTimer = flashDuration;
    }

    void OnCollisionEnter(Collision coll)
    {
        Health health = coll.gameObject.GetComponent<Health>();
        if (health != null)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                health.takeDamage(collisionDamage);
            }
            if (coll.gameObject.CompareTag("Enemy"))
            {
                //to be implemented
            }
        }
    }
}
