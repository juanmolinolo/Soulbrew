using UnityEngine;

public class PatrollerAttackZone : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasCollidedWithPlayer(collision))
        {
            player.GetComponent<HeroKnight>().TakeDamage(damage);
        }
    }

    private bool HasCollidedWithPlayer(Collider2D collision)
    {
        return collision.gameObject == player;
    }
}
