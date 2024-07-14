using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : IHealth, IEntity
{
    public float health { get ; set; }
    public float remainingHealth { get ; set; }
    public Queue<StatusEffect> statusEffects { get ; set; }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
