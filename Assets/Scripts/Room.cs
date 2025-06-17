using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour, IRayInteraction
{
    public List<GameObject> characterList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (!characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Add(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.EnterRoom(this.gameObject,false);
            }
        }
        if (!enemyList.Contains(other.gameObject) && other.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(other.gameObject);
            foreach (var human in characterList)
            {
                human.GetComponent<Employee>().FindEnemy();
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Remove(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.LeaveRoom();
            }
            
        }
        //todo: 방 나가도 계속 쫒아오는 문제 ㅎ결
        // if (enemyList.Contains(other.gameObject) && other.gameObject.CompareTag("Enemy"))
        // {
        //     enemyList.Remove(other.gameObject);
        //     foreach (var human in characterList)
        //     {
        //         human.GetComponent<Employee>().StopWalking();
        //         // human.GetComponent<Employee>().LeaveRoom();
        //     }
        // }
    }

    public virtual bool RayInteract()
    {
        return true;
    }

    public void MoveCharacter(GameObject point)
    {
        
    }
}
