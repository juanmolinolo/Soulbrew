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

    public void TakeDamage(int damage)
    {
        animator.SetTrigger(takeHitTrigger);
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
}
