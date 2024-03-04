
using UnityEngine;

public class ActionWander : FSMAction
{
    [Header("Wander Configuration")]
    [SerializeField]
    [Tooltip("Enemy wander area as the width and height of a box in Unity Units")]
    private Vector2 moveRange;
    [SerializeField] private float moveSpeed;
    [SerializeField]
    [Tooltip("How long enemy wanders in one direction before changing direction")]
    private float wanderTime;

    float timer;
    Vector3 movePosition;

    private void Start()
    {
        GetNewMoveDirection();
    }
    public override void Act()
    {
        Move();

        ResetTimer();
    }

    private void Move()
    {
        timer -= Time.deltaTime;
        Vector2 moveDirection = (movePosition - transform.position).normalized;
        float distanceToTarget = Vector2.Distance(movePosition, transform.position);
        if ((distanceToTarget >= 0.5f))//ToDo:change magic number to field
        {
            Vector2 movement = moveDirection * (moveSpeed * Time.deltaTime);
            transform.Translate(movement);
        }
    }
    private void ResetTimer()
    {
        if (timer <= 0f)
        {
            timer = wanderTime;
            GetNewMoveDirection();
        }
    }

    /// <summary>
    /// Calculates random new position within 
    /// a box determined by MoveRange
    /// </summary>
    private void GetNewMoveDirection()
    {
        float randomPosX = Random.Range(-moveRange.x,
            moveRange.x);
        float randomPosY = Random.Range(-moveRange.y,
            moveRange.y);
        movePosition = transform.position + new Vector3(randomPosX, randomPosY);
    }
    private void OnDrawGizmosSelected()
    {
        if (moveRange != Vector2.zero)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, moveRange);
            Gizmos.DrawLine(transform.position, movePosition);
        }
    }
}
