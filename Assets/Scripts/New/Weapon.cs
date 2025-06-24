using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public float attackDamage;
   public GameObject attacker;
   public void OnTriggerStay(Collider other)
   {
      // Debug.Log(other.gameObject.name);
      if (other.gameObject.CompareTag("Enemy"))
      {
         Debug.Log("아니 이거 뭔데!");
         if (other.TryGetComponent(out IDamagable damagable))
         {
            Debug.Log(damagable + " 으아 시이이이발");
            damagable.TakeDamage(5f,attacker);
         }
      }
   }
}
