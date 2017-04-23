using UnityEngine;

public class Shooter : MonoBehaviour
{
    public KeyCode shootKey;
    public bool playerControlled;
    public GameObject bulletPrefab;
    public float cooldown;
    public float damageMultiplier;
    public float bulletSpawnOffset;

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
	void Update ()
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

                    GameObject bulletObject = Instantiate(bulletPrefab);

                    Bullet bullet = bulletObject.GetComponent<Bullet>();

                    Vector3 shootDirection = Input.mousePosition;
                    shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                    shootDirection = shootDirection - transform.position;
                    shootDirection.y = 0;
                    shootDirection = shootDirection.normalized;
                    bullet.direction = shootDirection;


                    bullet.transform.position = transform.position + bulletSpawnOffset * shootDirection;

                    bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.up);

                    bullet.damage *= damageMultiplier;
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

                GameObject bulletObject = Instantiate(bulletPrefab);

                Bullet bullet = bulletObject.GetComponent<Bullet>();

                bullet.transform.position = transform.position;
                
                Vector3 shootDirection = targetTransform.position - transform.position;
                shootDirection = shootDirection.normalized;
                bullet.direction = shootDirection;

                bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.up);

                bullet.damage *= damageMultiplier;
            }
        }
	}
}
