using Assets.Scripts.Constants;
using Assets.Scripts.Enemies.Shared;
using UnityEngine;

public class HitBox : MonoBehaviour, IDamageableEnemy
{
    [SerializeField]
    private PatrolAndChase enemyMovement;

    [SerializeField]
    private SlashAttackZone enemyAttackZone;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int health;

    [SerializeField]
    private string takeHitTrigger;

    [SerializeField]
    private float takeHitDuration;

    public void TakeDamage(int damage)
    {
        TakeAHit();
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool(EnemyConstants.IS_DEAD_PARAMETER, true);
        Destroy(gameObject);
        Destroy(enemyMovement);
        Destroy(enemyAttackZone);
    }

    private void TakeAHit()
    {
        enemyAttackZone.SetTakingAHit(true);
        animator.SetTrigger(takeHitTrigger);
        Invoke(nameof(UnblockAttack), takeHitDuration);
    }

    private void UnblockAttack()
    {
        enemyAttackZone.SetTakingAHit(false);
    }
}
