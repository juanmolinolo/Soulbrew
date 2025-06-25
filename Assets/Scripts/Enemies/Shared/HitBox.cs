using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private Movement enemyMovement;

    private int health = 10;

    public void TakeDamage()
    {
        enemyMovement.PerformMovementBlockingAction(1f, "TakeHit");
        health--;
        Debug.Log($"Enemy health: {health}");
    }
}
