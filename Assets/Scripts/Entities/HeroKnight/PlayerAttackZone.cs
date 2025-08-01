﻿using Assets.Scripts.Enemies.Shared;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    private readonly List<IDamageableEnemy> enemyHitBoxesOnAttackZone = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageableEnemy>(out var hitBox))
        {
            enemyHitBoxesOnAttackZone.Add(hitBox);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageableEnemy>(out var hitBox))
        {
            enemyHitBoxesOnAttackZone.Remove(hitBox);
        }
    }

    public void AttackEnemiesInRange()
    {
        for (int i = 0; i < enemyHitBoxesOnAttackZone.Count; i++)
        {
            enemyHitBoxesOnAttackZone[i].TakeDamage(1);
        }
    }
}
