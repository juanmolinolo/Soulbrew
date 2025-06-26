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

    private int health = 2;

    public void TakeDamage()
    {
        animator.SetTrigger(AnimationConstants.TAKE_HIT_TRIGGER);
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool(AnimationConstants.IS_DEAD_PARAMETER, true);
        Destroy(gameObject);
        Destroy(enemyMovement);
        Destroy(enemyAttackZone);
    }
}
