using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;
    [SerializeField] private int healthPoints = 100;
    
     public int HealthPoints
    {
        get => healthPoints;
        private set => healthPoints = value;
    }
    
    public void DoDamage(int damage)
    {
        HealthPoints -= damage;
        if (HealthPoints <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerDeathEvent.RaiseEvent();
            enabled = false;
            Time.timeScale = 0;
        }
    }
}