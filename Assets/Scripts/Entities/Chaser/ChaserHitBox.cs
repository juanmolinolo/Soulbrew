using Assets.Scripts.Enemies.Shared;
using UnityEngine;

public class ChaserHitBox : MonoBehaviour, IDamageableEnemy
{
    [SerializeField]
    private ChaserMovement enemyMovement;

    [SerializeField]
    private ChaserAttackZone enemyAttackZone;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int health;

    [SerializeField]
    private string takeHitTrigger;

    [SerializeField]
    private float takeHitDuration;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip hurtSound;

    [SerializeField]
    private AudioClip deathSound;

    public void TakeDamage(int damage)
    {
        TakeAHit();
        health -= damage;
        if (health <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            Die();
        }
        else
        {
            audioSource.PlayOneShot(hurtSound);
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
