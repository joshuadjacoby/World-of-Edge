using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float moveAcceleration;
    public float maxMoveSpeed;
    public float friction;

    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("NeutralBullet"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("NeutralBullet"), LayerMask.NameToLayer("NeutralBullet"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("Player"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"));
    }

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

        navMeshAgent.velocity = navMeshAgent.velocity + moveDir * moveAcceleration * Time.deltaTime
                                                      - navMeshAgent.velocity * friction * Time.deltaTime;
        if (navMeshAgent.velocity.magnitude > maxMoveSpeed)
        {
            navMeshAgent.velocity = navMeshAgent.velocity.normalized * maxMoveSpeed;
        }
    }
}
