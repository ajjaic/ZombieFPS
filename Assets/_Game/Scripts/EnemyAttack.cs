using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private PlayerHealth targetToAttack;
    
    public void AttackTarget()
    {
        targetToAttack.DoDamage(meleeDamage); 
    }
}
