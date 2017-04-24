using UnityEngine;

public enum ShootType
{
    Single,
    Multi,
    Sin
}

public class Shooter : MonoBehaviour
{
    public Transform avatarTransform;
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
    public AudioSource shootSound;

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
                    if (shootSound != null)
                    {
                        shootSound.Play();
                    }
                    cooldownTimer = cooldown;
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = Camera.main.transform.position.y - transform.position.y;
                    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    mouseWorldPosition.y = transform.position.y;
                    Vector3 shootDirection = (mouseWorldPosition - transform.position).normalized;

                    //Debug.DrawRay(transform.position, shootDirection * 100, Color.red);

                    playerAttack(shootDirection);
                }
            }
        }
        else
        {
            Vector3 shootDirection = targetTransform.position - transform.position;
            shootDirection = shootDirection.normalized;
            avatarTransform.LookAt(shootDirection, Vector3.up);

            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                cooldownTimer = cooldown;
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
                    shootDirection = Quaternion.Euler(0, degreesBetweenBullets / 2 - (numBullets / 2) * degreesBetweenBullets, 0) * shootDirection;
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
            case ShootType.Sin:
                fireSinBullets(shootDirection, equippedBulletPrefabIndex);
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
    public void fireSinBullets(Vector3 shootDirection, int type)
    {
        for(int i = 0; i < numBullets; i++)
        {
            GameObject bulletObject = (GameObject)Instantiate(bulletPrefab[type]);
            SinBullet bullet = bulletObject.GetComponent<SinBullet>();
            bullet.direction = shootDirection;
            bullet.direction += new Vector3(Random.Range(0.0f, spreadVariance), Random.Range(0.0f, spreadVariance), 0.0f);

            bullet.transform.position = transform.position + bulletSpawnOffset * shootDirection;

            bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.up);
            //bullet.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f, 2000.0f, 0.0f));
            bullet.damage *= damageMultiplier;
            if (i%2 == 0)
            {
                bullet.flipX = true;
            }
        }
    }
}
