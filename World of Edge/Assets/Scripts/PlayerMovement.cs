using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rbody;
    public float moveAcceleration;
    public float maxMoveSpeed;
    public float friction;

    public void Update()
    {
        Vector3 moveDir = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir += Vector3.right;
        }

        rbody.velocity = rbody.velocity + moveDir * moveAcceleration * Time.deltaTime
                                        - rbody.velocity * friction * Time.deltaTime;
        if (rbody.velocity.magnitude > maxMoveSpeed)
        {
            rbody.velocity = rbody.velocity.normalized * maxMoveSpeed;
        }
    }
}
