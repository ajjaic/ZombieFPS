using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints = 100;
    private Animator _animator;

     public int HealthPoints
    {
        get => healthPoints;
        private set => healthPoints = value;
    }
     
    // messages
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // API
    public int GetCurrentHealth()
    {
        return HealthPoints;
    }
    
    public void DoDamage(int damage)
    {
        HealthPoints -= damage;
        if (HealthPoints < 0)
        {
            GetComponent<EnemyAI>().enabled = false;
            _animator.SetTrigger("DeadTrigger");
        }
    }
}
