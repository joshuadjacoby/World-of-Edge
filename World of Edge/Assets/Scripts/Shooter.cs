using UnityEngine;

public class Shooter : MonoBehaviour
{
    public KeyCode shootKey;
    public GameObject bulletPrefab;
    public float cooldown;

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

                // TODO : FIX THIS SHIST
                Instantiate(bulletPrefab);
                Bullet bullet = bulletPrefab.GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.y);
                bullet.direction = (Camera.main.ScreenToWorldPoint(screenPoint) - transform.position).normalized;
                Debug.Log(bullet.direction);
                bullet.transform.rotation = Quaternion.LookRotation(bullet.direction, Vector3.down);
            }
        }
	}
}
