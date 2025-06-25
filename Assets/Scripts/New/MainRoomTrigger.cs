using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name+ " ?????????????????????????/");
        if (other.gameObject.CompareTag("Enemy"))
        {
          GameManager.Instance.MonsterEnter(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.MonsterDie(other.gameObject);
        }
    }

}
