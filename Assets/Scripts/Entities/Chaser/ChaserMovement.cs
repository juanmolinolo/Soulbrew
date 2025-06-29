using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMovement : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<GameObject> patrolPoints;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float playerStopDistance;

    [SerializeField]
    private float obstacleCheckDistance;

    [SerializeField]
    private LayerMask obstacleLayerMask;

    #endregion Parameters

    private bool isWhithinChaseDistance = false;
    private bool isWithinPlayerStopDistance = false;
    private int currentPatrolIndex = 0;
    private Vector2 targetPosition;

    private void Start()
    {
        SetFirstPatrolPointTarget();
        VariateMovementSpeed();
    }

    void Update()
    {
        isWhithinChaseDistance = IsWithinChaseDistance();
        isWithinPlayerStopDistance = IsWithinPlayerStopDistance();

        bool shouldMove = false;
        if (isWhithinChaseDistance)
        {
            if (!isWithinPlayerStopDistance)
            {
                if (transform.position.x > player.transform.position.x)
                {
                    shouldMove = !IsObstacleInDirection(Direction.Left);
                }
                else if (transform.position.x < player.transform.position.x)
                {
                    shouldMove = !IsObstacleInDirection(Direction.Right);
                }
            }
        }
        else
        {
            Vector2 currentPosition = transform.position;
            if (currentPosition.x < targetPosition.x)
            {
                shouldMove = !IsObstacleInDirection(Direction.Right);
            }
            else if (currentPosition.x > targetPosition.x)
            {
                shouldMove = !IsObstacleInDirection(Direction.Left);
            }
        }

        animator.SetBool(EnemyConstants.IS_MOVING_PARAMETER, shouldMove);

        if (isWhithinChaseDistance)
        {
            if (!isWithinPlayerStopDistance)
            {
                Chase();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Chase()
    {
        if (transform.position.x > player.transform.position.x)
        {
            if (!IsObstacleInDirection(Direction.Left))
            {
                MoveEntity(Direction.Left);
            }
        }
        else if (transform.position.x < player.transform.position.x)
        {
            if (!IsObstacleInDirection(Direction.Right))
            {
                MoveEntity(Direction.Right);
            }
        }
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
            if (!IsObstacleInDirection(Direction.Right))
            {
                MoveEntity(Direction.Right);
            }
        }
        else if (currentPosition.x > targetPosition.x)
        {
            if (!IsObstacleInDirection(Direction.Left))
            {
                MoveEntity(Direction.Left);
            }
        }
    }

    private bool IsWithinChaseDistance()
    {
        float horizontalDistance = Mathf.Abs(transform.position.x - player.transform.position.x);
        return horizontalDistance <= EnemyConstants.CHASE_DISTANCE;
    }

    private bool IsWithinPlayerStopDistance()
    {
        float horizontalDistance = Mathf.Abs(transform.position.x - player.transform.position.x);
        return horizontalDistance <= playerStopDistance;
    }

    private bool IsObstacleInDirection(Direction direction)
    {
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = direction == Direction.Right ? Vector2.right : Vector2.left;

        float[] rayHeights = { 0f, 0.5f, -0.5f };

        foreach (float heightOffset in rayHeights)
        {
            Vector2 adjustedOrigin = rayOrigin + Vector2.up * heightOffset;
            RaycastHit2D hit = Physics2D.Raycast(adjustedOrigin, rayDirection, obstacleCheckDistance, obstacleLayerMask);
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
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

    private void VariateMovementSpeed()
    {
        float speedMultiplier = Random.Range(1f - EnemyConstants.SPEED_VARIATION_PERCENTAGE, 1f + EnemyConstants.SPEED_VARIATION_PERCENTAGE);
        movementSpeed *= speedMultiplier;
    }
}