﻿using UnityEngine;

public enum ShootType
{
    Single,
    Multi
}

public class Shooter : MonoBehaviour
{
    public KeyCode shootKey;
    public bool playerControlled;
    public GameObject[] bulletPrefab;
    public float cooldown;
    public float damageMultiplier;
    public float bulletSpawnOffset;
    public float spreadVariance;
    public int equippedBulletPrefabIndex;
    public ShootType howToShotBullet;
    public int numBullets = 1;
    public float degreesBetweenBullets = 10f;

    private float cooldownTimer;
    private Transform targetTransform;

    void Start()
    {
        cooldownTimer = cooldown;
        if (!playerControlled)
        {
            PlayerMovement playerMove = FindObjectOfType<PlayerMovement>();
            if (playerMove != null)
            {
                targetTransform = playerMove.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlled)
        {
            if (Input.GetKey(shootKey))
            {
                if (cooldownTimer > 0)
                {
                    cooldownTimer -= Time.deltaTime;
                }
                else
                {
                    cooldownTimer = cooldown;
                    Vector3 shootDirection = Input.mousePosition;
                    shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                    shootDirection = shootDirection - transform.position;
                    shootDirection.y = 0;
                    shootDirection = shootDirection.normalized;
                    playerAttack(shootDirection);
                }
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.position - targetTransform.position, Vector3.up);

            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                cooldownTimer = cooldown;
                Vector3 shootDirection = targetTransform.position - transform.position;
                shootDirection = shootDirection.normalized;
                fireBullet(shootDirection, 0);

            }
        }
    }

    public void playerAttack(Vector3 shootDirection)
    {
        switch (howToShotBullet)
        {
            case (ShootType.Single):
                fireBullet(shootDirection, equippedBulletPrefabIndex);
                break;
            case (ShootType.Multi):
                if (numBullets % 2 == 0)
                {
                    shootDirection = Quaternion.Euler(0, degreesBetweenBullets / 2 - (numBullets / 2 - 1) * degreesBetweenBullets, 0) * shootDirection;
                    for (int i = 0; i < numBullets; i++)
                    {
                        fireBullet(shootDirection, equippedBulletPrefabIndex);
                        shootDirection = Quaternion.Euler(0, degreesBetweenBullets, 0) * shootDirection;
                    }
                }
                else
                {
                    shootDirection = Quaternion.Euler(0, -(numBullets / 2) * degreesBetweenBullets, 0) * shootDirection;
                    for (int i = 0; i < numBullets; i++)
                    {
                        fireBullet(shootDirection, equippedBulletPrefabIndex);
                        shootDirection = Quaternion.Euler(0, degreesBetweenBullets, 0) * shootDirection;
                    }
                }
                break;
        }
    }

    public void fireBullet(Vector3 shootDirection, int type)
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab[type]);
        BulletParent bullet = bulletObject.GetComponent<BulletParent>();
        bullet.direction = shootDirection;
        bullet.direction += new Vector3(Random.Range(0.0f, spreadVariance), Random.Range(0.0f, spreadVariance), 0.0f);

        bullet.transform.position = transform.position + bulletSpawnOffset * shootDirection;

        bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.up);
        //bullet.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f, 2000.0f, 0.0f));
        bullet.damage *= damageMultiplier;
    }
}
