using UnityEngine;

public class Shooter : MonoBehaviour
{
    public KeyCode shootKey;
    public GameObject bulletPrefab;
    public float cooldown;
    public float damageMultiplier;

    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = cooldown;
    }

	// Update is called once per frame
	void Update ()
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

                bullet.transform.position = transform.position;

                Vector3 shootDirection = Input.mousePosition;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection - transform.position;
                shootDirection.y = 0;
                shootDirection = shootDirection.normalized;
                Debug.DrawRay(transform.position, shootDirection);
                bullet.direction = shootDirection;
                
                bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.up);

                bullet.damage *= damageMultiplier;
            }
        }
	}
}
