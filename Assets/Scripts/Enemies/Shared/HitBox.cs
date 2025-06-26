using Assets.Scripts.Constants;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private Movement enemyMovement;

    [SerializeField]
    private AttackZone enemyAttackZone;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int health;

    public void TakeDamage(int amount)
    {
        animator.SetTrigger(EnemyConstants.TAKE_HIT_TRIGGER);
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool(EnemyConstants.IS_DEAD_PARAMETER, true);
        Destroy(gameObject);
        Destroy(enemyMovement);
        Destroy(enemyAttackZone);
    }
}
