using UnityEngine;

public class Shooter : MonoBehaviour
{
    public enum BulletType
    {
        normal,
        piercing,
        ricochet,
        explosive,
        spreadShot2,
        spreadShot3,
        spreadShot4,
        spreadShot5
    }
    public KeyCode shootKey;
    public bool playerControlled;
    public GameObject[] bulletPrefab;
    public float cooldown;
    public float damageMultiplier;
    public float bulletSpawnOffset;

    private float cooldownTimer;
    private Transform targetTransform;
    private Player player;
    void Start()
    {
        cooldownTimer = cooldown;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        BulletType bullet = (BulletType)player.getGunType();
        //hardcode for testing
        //bullet = BulletType.explosive;
        switch (bullet)
        {
            case (BulletType.normal):
                fireBullet(shootDirection, (int)BulletType.normal, true, true);
                break;
            case (BulletType.piercing):
                fireBullet(shootDirection, (int)BulletType.piercing, false, false);
                break;
            case (BulletType.ricochet):
                fireBullet(shootDirection, (int)BulletType.ricochet, true, true);
                break;
            case (BulletType.explosive):
                fireBullet(shootDirection, (int)BulletType.explosive, true, false);
                break;
            case (BulletType.spreadShot2):
                multiFire(shootDirection, (int)BulletType.spreadShot2, 2, 10, true);
                break;
            case (BulletType.spreadShot3):
                multiFire(shootDirection, (int)BulletType.spreadShot3, 3, 10, true);
                break;
            case (BulletType.spreadShot4):
                multiFire(shootDirection, (int)BulletType.spreadShot4, 4, 10, true);
                break;
            case (BulletType.spreadShot5):
                multiFire(shootDirection, (int)BulletType.spreadShot5, 5, 10, true);
                break;
        }
    }

    public void fireBullet(Vector3 shootDirection, int type, bool bulletSpread = false, bool shapeOverride = false)
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab[type]);
        if (shapeOverride)
        {
            Object[] sprites;
            sprites = Resources.LoadAll("Projectiles");



            int bulletSpriteIndex = Random.Range(1, sprites.Length);

            bulletObject.GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)sprites[bulletSpriteIndex];
        }
        
        BulletParent bullet = bulletObject.GetComponent<BulletParent>();
        bullet.direction = shootDirection;
        if (bulletSpread)
            bullet.direction += new Vector3(Random.Range(0.0f, 0.2f), Random.Range(0.0f, 0.2f), 0.0f);

        bullet.transform.position = transform.position + bulletSpawnOffset * shootDirection;

        bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.up);
        //bullet.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f, 2000.0f, 0.0f));
        bullet.damage *= damageMultiplier;
    }
    public void multiFire(Vector3 shootDirection, int type, int numBullets, float degreesBetweenBullets = 10f, bool shapeOverride = false)
    {
        if (numBullets % 2 == 0)
        {
            shootDirection = Quaternion.Euler(0, degreesBetweenBullets / 2 - (numBullets / 2 - 1) * degreesBetweenBullets, 0) * shootDirection;
            for (int i = 0; i < numBullets; i++)
            {
                fireBullet(shootDirection, type, false, shapeOverride);
                shootDirection = Quaternion.Euler(0, degreesBetweenBullets, 0) * shootDirection;
            }
        }
        else
        {
            shootDirection = Quaternion.Euler(0, -(numBullets / 2) * degreesBetweenBullets, 0) * shootDirection;
            for (int i = 0; i < numBullets; i++)
            {
                fireBullet(shootDirection, type, false, shapeOverride);
                shootDirection = Quaternion.Euler(0, degreesBetweenBullets, 0) * shootDirection;
            }
        }
    }
}
