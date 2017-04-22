using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;
    public float speed;
    public Vector3 direction;
    public Rigidbody rbody;

	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        transform.position += direction * speed;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
