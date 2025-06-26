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
    private List<string> attackTriggerOptions;

    [SerializeField]
    private Animator animator;

    private float lastAttackTime = 0f;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasCollidedWithPlayer(collision.gameObject))
        {
            StopMovement();
            if (ShouldAttack())
            {
                isPlayerInRange = true;
                Attack();
            }
            else
            {
                Invoke(nameof(ResetAttackRange), EnemyConstants.ATTACK_DURATION);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (HasCollidedWithPlayer(collision.gameObject))
        {
            isPlayerInRange = false;
        }
    }

    private bool HasCollidedWithPlayer(GameObject gameObject)
    {
        return gameObject == player;
    }

    private void StopMovement()
    {
        animator.SetBool(AnimationConstants.IS_MOVING_PARAMETER, false);
        enemyMovement.enabled = false;
    }

    private bool ShouldAttack()
    {
        return Time.time - lastAttackTime > EnemyConstants.ATTACK_COOLDOWN;
    }

    private void Attack()
    {
        animator.SetTrigger(GetRandomAttackTrigger());
        lastAttackTime = Time.time;
        Invoke(nameof(DealDamageIfInRange), EnemyConstants.ATTACK_DURATION);
        Invoke(nameof(ResetAttackRange), EnemyConstants.ATTACK_DURATION);
    }

    private string GetRandomAttackTrigger()
    {
        int randomIndex = Random.Range(0, attackTriggerOptions.Count);
        return attackTriggerOptions[randomIndex];
    }

    private void DealDamageIfInRange()
    {
        if (isPlayerInRange)
        {
            player.GetComponent<HeroKnight>().TakeDamage();
        }
    }

    private void ResetAttackRange()
    {
        enemyMovement.enabled = true;
    }
}

