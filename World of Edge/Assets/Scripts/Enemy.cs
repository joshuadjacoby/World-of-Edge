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
    private bool AIActive = true;
    protected GameObject player;
    protected int damage;
    protected int enemyType;
    private const float PARTICLE_LIFETIME = 2f;

    void Start()
    {

    }

    void Update()
    {
        if (health.health <= 0)
        {
            kill();
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

    public void kill()
    {
        deglue();
        StartCoroutine(destroyAfterDelay(PARTICLE_LIFETIME));
    }

    public IEnumerator destroyAfterDelay(float duration)
    {
        Debug.Log("ASDASD");
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

    void OnCollisionEnter(Collision coll)
    {
        Health health = coll.gameObject.GetComponent<Health>();
        if (health != null)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                health.health -= damage;
            }
            if (coll.gameObject.CompareTag("Enemy"))
            {
                //to be implemented
            }
        }
    }
}
