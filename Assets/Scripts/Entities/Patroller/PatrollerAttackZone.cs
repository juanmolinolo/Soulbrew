using UnityEngine;

public class PatrollerAttackZone : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int attackDamage;

    private HeroKnight heroKnight;

    private void Start()
    {
        heroKnight = player.GetComponent<HeroKnight>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasCollidedWithPlayer(collision))
        {
            heroKnight.TakeDamage(attackDamage);
        }
    }

    private bool HasCollidedWithPlayer(Collider2D collision)
    {
        return collision.gameObject == player;
    }
}
