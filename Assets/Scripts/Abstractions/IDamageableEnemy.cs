namespace Assets.Scripts.Enemies.Shared
{
    interface IDamageableEnemy
    {
        void TakeDamage(int damage);
        void Die();
    }
}
