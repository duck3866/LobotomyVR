using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage, GameObject attacker);
    public void TakeDamage(float damage);
}
