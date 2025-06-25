using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private Movement enemyMovement;

    private int health = 2;

    public void TakeDamage()
    {
        if (!enemyMovement.isPerformingMovementBlockingAction)
        {
            enemyMovement.PerformMovementBlockingAction(1f, "TakeHit");
            health--;
            Debug.Log($"Enemy health: {health}");
            if (health == 0)
            {
                Invoke(nameof(Die), 1f);
            }
        }
    }

    private void Die()
    {
        enemyMovement.PerformMovementBlockingAction(1f, "Die");
        Invoke(nameof(DestroyObject), 1f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject.GetComponentInParent<SpriteRenderer>());
    }
}
