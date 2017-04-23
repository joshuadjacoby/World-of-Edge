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
    public MeshRenderer meshRenderer;
    public float flashDuration = 0.15f;
    public EnemyManager enemyManager;
    private bool AIActive = true;
    protected GameObject player;
    protected int damage;
    protected int enemyType;
    public float deathDelay;
    public int edgesToSpawn;
    private float flashTimer;

    public void DoUpdate(float deltaTime)
    {
        if (flashTimer > 0)
        {
            float colorDiff = 1 - flashTimer / flashDuration;
            meshRenderer.material.SetColor("_Color", new Color(1, colorDiff, colorDiff));
            flashTimer -= deltaTime;
        }
        else
        {
            meshRenderer.material.SetColor("_Color", Color.white);
            flashTimer = 0;
        }
        if (health.currentHealth <= 0)
        {
            //deglue();
            //StartCoroutine(destroyAfterDelay(deathDelay));
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
            GameObject edge = (GameObject)Instantiate(Resources.Load("Prefabs/Edge"));
            float rngAngle = Random.Range(0, 360);
            float rngSpeed = Random.Range(3, 10);
            Vector3 rngVector = new Vector3(Mathf.Cos(rngAngle), 0, Mathf.Sin(rngAngle));
            Debug.DrawRay(transform.position, rngVector, Color.green, 2f);
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
        meshRenderer.material.SetColor("_Color", Color.red);
        flashTimer = flashDuration;
    }

    void OnCollisionEnter(Collision coll)
    {
        Health health = coll.gameObject.GetComponent<Health>();
        if (health != null)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                health.takeDamage(damage);
            }
            if (coll.gameObject.CompareTag("Enemy"))
            {
                //to be implemented
            }
        }
    }
}
