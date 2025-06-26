using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.HeroKnight
{
    public class PlayerAttackZone : MonoBehaviour
    {
        private readonly List<HitBox> enemyHitBoxesOnAttackZone = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<HitBox>(out var hitBox))
            {
                enemyHitBoxesOnAttackZone.Add(hitBox);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<HitBox>(out var hitBox))
            {
                enemyHitBoxesOnAttackZone.Remove(hitBox);
            }
        }

        public void AttackEnemiesInRange()
        {
            for (int i = 0; i < enemyHitBoxesOnAttackZone.Count; i++)
            {
                enemyHitBoxesOnAttackZone[i].TakeDamage();
            }
        }
    }
}
