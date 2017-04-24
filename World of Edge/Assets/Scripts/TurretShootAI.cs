using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootAI : Enemy {

    // Use this for initialization
    private bool LOS;
    private const float LOSREPEAT = 0.2f;
    public float fireDelay;
    private float fireTimer;
    void Start ()
    {
        DoStart();
        fireTimer = 0;
        StartCoroutine(LOSLoop());
	}
	
	// Update is called once per frame
	void Update () {
        if (LOS)
        {
            aimAt(player.transform.position);
            if(fireTimer < fireDelay)
            {
                fireTimer += Time.deltaTime;
            } else
            {
                fireTimer = 0;
                fireAt(player.transform.position);
            }
        } else
        {
            fireTimer = 0;
        }
        DoUpdate(Time.deltaTime);
	}
    private IEnumerator LOSLoop()
    {
        LOS = hasLOS(player.transform.position);
        yield return new WaitForSeconds(LOSREPEAT);
        StartCoroutine(LOSLoop());
    }

    private bool hasLOS(Vector3 dest)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dest - transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
                return true;
        }
        return false;

    }
    private void fireAt(Vector3 target)
    {
    }
    private void aimAt(Vector3 target)
    {
        Vector3 aimVector = target - transform.position;
        float step = 5 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, aimVector, step, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}
