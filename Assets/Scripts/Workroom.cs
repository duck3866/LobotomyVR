using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workroom : Room
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private bool isWalking = false;
    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (!characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Add(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.EnterRoom(this.gameObject,true);
                characterList[0].GetComponent<Employee>().Work(walkSpeed);
            }
            isWalking = true;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Remove(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.LeaveRoom();
            }
            isWalking = false;
        }
        
    }

    public override bool RayInteract()
    {
        if (isWalking)
        {
            return false;
        }
        return true;
    }
}
