using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<GameObject> patrolPoints;

    [SerializeField]
    private GameObject player;

    #endregion Parameters

    private bool isWhithinChaseDistance = false;
    private int currentPatrolIndex = 0;
    private Vector2 targetPosition;

    private void Start()
    {
        SetFirstPatrolPointTarget();
    }

    void Update()
    {
        animator.SetBool(EnemyConstants.IS_MOVING_PARAMETER, true);
        if (isWhithinChaseDistance)
        {
            Chase();
        }
        else
        {
            Patrol();
        }

        isWhithinChaseDistance = ShouldChase();
    }

    private void Chase()
    {
        if (transform.position.x > player.transform.position.x)
        {
            MoveEntity(Direction.Left);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            MoveEntity(Direction.Right);
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
            MoveEntity(Direction.Right);
        }
        else if (currentPosition.x > targetPosition.x)
        {
            MoveEntity(Direction.Left);
        }
    }

    private bool ShouldChase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= EnemyConstants.CHASE_DISTANCE;
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
            currentPosition.x -= EnemyConstants.MOVEMENT_SPEED * Time.deltaTime;
        }
        else if (direction == Direction.Right)
        {
            if (transform.localScale.x < 0)
            {
                TurnAround();
            }
            currentPosition.x += EnemyConstants.MOVEMENT_SPEED * Time.deltaTime;
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
