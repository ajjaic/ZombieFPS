using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int HealthPoints { get; }

    void DoDamage(int damage);
}

