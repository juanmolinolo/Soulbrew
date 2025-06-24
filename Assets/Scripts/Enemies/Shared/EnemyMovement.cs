using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<GameObject> patrolPoints;

    private bool isMoving = false;
    private int currentPatrolIndex = 0;
    private Vector2 targetPosition;

    private void Start()
    {
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            targetPosition = patrolPoints[0].transform.position;
        }
    }

    void Update()
    {
        Vector2 currentPosition = transform.position;
        float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

        if (distanceToTarget < EnemyConstants.PATROL_MARGIN)
        {
            CyclePatrol();
        }

        if (currentPosition.x < targetPosition.x)
        {
            MoveEntity(Direction.Right);
        }
        else if (currentPosition.x > targetPosition.x)
        {
            MoveEntity(Direction.Left);
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, isMoving);
    }

    private void CyclePatrol()
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
            currentPosition.x -= EnemyConstants.ENEMY_SPEED * Time.deltaTime;
        }
        else if (direction == Direction.Right)
        {
            if (transform.localScale.x < 0)
            {
                TurnAround();
            }
            currentPosition.x += EnemyConstants.ENEMY_SPEED * Time.deltaTime;
        }

        transform.position = currentPosition;
        isMoving = true;
    }

    private void TurnAround()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
