using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> characterList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (!characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Add(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.EnterRoom(this.gameObject);
            }
        }
        if (!enemyList.Contains(other.gameObject) && other.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Remove(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.LeaveRoom();
            }
            
        }
        if (enemyList.Contains(other.gameObject) && other.gameObject.CompareTag("Enemy"))
        {
            enemyList.Remove(other.gameObject);
        }
    }
}
