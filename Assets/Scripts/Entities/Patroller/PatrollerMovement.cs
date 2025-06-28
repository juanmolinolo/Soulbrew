using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerMovement : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> patrolPoints;

    [SerializeField]
    private float movementSpeed;

    private int currentPatrolIndex = 0;
    private Vector2 targetPosition;

    void Start()
    {
        SetFirstPatrolPointTarget();
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector2 currentPosition = transform.position;
        float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

        if (distanceToTarget < EnemyConstants.PATROL_MARGIN)
        {
            ChangePatrolPoint();
        }

        if (currentPosition.x < targetPosition.x)
        {
            MoveEntity(Direction.Right);
        }
        else if (currentPosition.x > targetPosition.x)
        {
            MoveEntity(Direction.Left);
        }
    }

    private void ChangePatrolPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        targetPosition = patrolPoints[currentPatrolIndex].transform.position;
    }

    private void MoveEntity(Direction direction)
    {
        Vector2 currentPosition = transform.position;

        if (direction == Direction.Left)
        {
            if (transform.localScale.x > 0)
            {
                TurnAround();
            }
            currentPosition.x -= movementSpeed * Time.deltaTime;
        }
        else if (direction == Direction.Right)
        {
            if (transform.localScale.x < 0)
            {
                TurnAround();
            }
            currentPosition.x += movementSpeed * Time.deltaTime;
        }

        transform.position = currentPosition;
    }

    private void TurnAround()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void SetFirstPatrolPointTarget()
    {
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            targetPosition = patrolPoints[0].transform.position;
        }
    }
}
