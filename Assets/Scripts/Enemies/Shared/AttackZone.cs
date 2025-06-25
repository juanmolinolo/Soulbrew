using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Movement enemyMovement;

    [SerializeField]
    private List<AnimationClip> attackAnimationOptions;

    private float lastAttackTime;
    private bool isInAttackZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !enemyMovement.isPerformingMovementBlockingAction)
        {
            if (Time.time - lastAttackTime > EnemyConstants.ATTACK_COOLDOWN)
            {
                isInAttackZone = true;
                enemyMovement.PerformMovementBlockingAction(EnemyConstants.ATTACK_DURATION, GetRandomAttackAnimationName());
                Invoke(nameof(CheckPlayerInAttackRange), EnemyConstants.ATTACK_DURATION / 2);
                lastAttackTime = Time.time;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            isInAttackZone = false;
        }
    }

    private string GetRandomAttackAnimationName()
    {
        int randomIndex = Random.Range(0, attackAnimationOptions.Count);
        return attackAnimationOptions[randomIndex].name;
    }

    private void CheckPlayerInAttackRange()
    {
        if (isInAttackZone)
        {
            player.GetComponent<HeroKnight>().TakeDamage();
        }
    }
}

