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
      Debug.Log("???/dsdadqdqdqdasnacaoc l");
      if (other.gameObject.CompareTag("Enemy"))
      {
         Debug.Log("아니 이거 뭔데!");
         if (other.TryGetComponent(out IDamagable damagable))
         {
            damagable.TakeDamage(5f,attacker);
         }
      }
   }
}
