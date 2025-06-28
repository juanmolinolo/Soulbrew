using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttackZone : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private PatrolAndChase enemyMovement;

    [SerializeField]
    private List<string> attackTriggers;

    [SerializeField]
    private List<float> attackDurations;

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private Animator animator;

    private float lastAttackTime = 0f;
    private bool isPlayerInRange = false;
    private bool isTakingAHit = false;

    private void Update()
    {
        if (isPlayerInRange && ShouldAttack())
        {
            StopMovement();
            Attack();
        }
    }

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
                Invoke(nameof(ResetAttackRange), GetRandomAttackDuration());
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HasCollidedWithPlayer(collision.gameObject))
        {
            isPlayerInRange = true;
        }
    }

    private bool HasCollidedWithPlayer(GameObject gameObject)
    {
        return gameObject == player;
    }

    private void StopMovement()
    {
        animator.SetBool(EnemyConstants.IS_MOVING_PARAMETER, false);
        enemyMovement.enabled = false;
    }

    private bool ShouldAttack()
    {
        return !isTakingAHit && (Time.time - lastAttackTime > attackCooldown);
    }

    private void Attack()
    {
        KeyValuePair<string, float> attackTriggerDuration = GetRandomAttackTriggerDurationKeyValuePair();

        animator.SetTrigger(attackTriggerDuration.Key);
        lastAttackTime = Time.time;
        Invoke(nameof(DealDamageIfInRange), attackTriggerDuration.Value);
        Invoke(nameof(ResetAttackRange), attackTriggerDuration.Value);
    }

    private KeyValuePair<string, float> GetRandomAttackTriggerDurationKeyValuePair()
    {
        int randomIndex = Random.Range(0, attackTriggers.Count);
        return new KeyValuePair<string, float>(attackTriggers[randomIndex], attackDurations[randomIndex]);
    }

    private float GetRandomAttackDuration()
    {
        int randomIndex = Random.Range(0, attackDurations.Count);
        return attackDurations[randomIndex];
    }

    private void DealDamageIfInRange()
    {
        if (isPlayerInRange && !isTakingAHit)
        {
            player.GetComponent<HeroKnight>().TakeDamage();
        }
    }

    private void ResetAttackRange()
    {
        enemyMovement.enabled = true;
    }

    public void SetTakingAHit(bool isTakingHit)
    {
        isTakingAHit = isTakingHit;
    }
}

