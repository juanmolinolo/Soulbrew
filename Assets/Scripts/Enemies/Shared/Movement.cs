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

    public bool isPerformingMovementBlockingAction = false;
    private bool shouldMove = true;
    private bool isChasing = false;

    private int currentPatrolIndex = 0;
    private Vector2 targetPosition;

    private void Start()
    {
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            targetPosition = patrolPoints[0].transform.position;
        }
        animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, true);
    }

    void Update()
    {
        if (!isPerformingMovementBlockingAction)
        {
            if (shouldMove)
            {
                animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, true);
                if (isChasing)
                {
                    Chase();
                }
                else
                {
                    Patrol();
                }
            }

            isChasing = ShouldChase();
        }
        else
        {
            animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, false);
        }
    }

    public void PerformMovementBlockingAction(float actionDuration, string animationTrigger = null)
    {
        if (!isPerformingMovementBlockingAction)
        {
            isPerformingMovementBlockingAction = true;
            if (animationTrigger != null)
            {
                animator.SetTrigger(animationTrigger);
            }
            Invoke(nameof(EndMovementBlockingAction), actionDuration);
        }
    }

    private void EndMovementBlockingAction()
    {
        isPerformingMovementBlockingAction = false;
    }

    private void Chase()
    {
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.transform.position.x);

        if (distanceToPlayerX > 0.05f)
        {
            shouldMove = true;

            if (transform.position.x > player.transform.position.x)
            {
                MoveEntity(Direction.Left);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                MoveEntity(Direction.Right);
            }
        }
        else
        {
            if (shouldMove)
            {
                shouldMove = false;
                animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, false);
                Invoke(nameof(EndWalkingCoolDown), EnemyConstants.WALKING_COOLDOWN);
            }
        }
    }

    private void EndWalkingCoolDown()
    {
        shouldMove = true;
        animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, true);
    }

    private void Patrol()
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
    }

    private bool ShouldChase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= EnemyConstants.CHASE_DISTANCE;
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
    }

    private void TurnAround()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
