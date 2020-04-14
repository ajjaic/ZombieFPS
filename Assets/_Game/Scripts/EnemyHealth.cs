using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints = 100;

     public int HealthPoints
    {
        get => healthPoints;
        private set => healthPoints = value;
    }

    // API
    public int GetCurrentHealth()
    {
        return HealthPoints;
    }
    
    public void DoDamage(int damage)
    {
        HealthPoints -= damage;
    }
}
