using Assets.Scripts.Enemies.Shared;
using UnityEngine;

public class PatrollerHitBox : MonoBehaviour, IDamageableEnemy
{
    [SerializeField]
    private int health;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private string deathAnimationTrigger;

    [SerializeField]
    private float deathAnimationLength;

    [SerializeField]
    private PatrollerAttackZone enemyAttackZone;

    [SerializeField]
    private PatrollerMovement enemyMovement;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private AudioClip hitSound;

    private Color originalColor;

    private void Start()
    {
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        FlashRed();
        health -= damage;
        if (health <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            Die();
        }
        else
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void FlashRed()
    {
        spriteRenderer.color = Color.red;
        Invoke(nameof(ResetColor), 0.1f);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

    public void Die()
    {
        enemyMovement.enabled = false;
        enemyAttackZone.enabled = false;
        animator.SetTrigger(deathAnimationTrigger);
        Invoke(nameof(DestroyEnemy), deathAnimationLength);
    }

    private void DestroyEnemy()
    {
        Destroy(enemyAttackZone);
        Destroy(spriteRenderer);
        Destroy(enemyMovement);
        Destroy(gameObject);
    }
}
