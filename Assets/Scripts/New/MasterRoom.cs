using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterRoom : MonoBehaviour
{
    public Animator doorAnimator;
    public bool playerInRoom = false;
    public bool isClear = false;
    public MonsterRoom monsterRoom;
    public void DoorAnimation()
    {
        if (!playerInRoom || isClear)
        {
            doorAnimator.SetTrigger("DoorToggle");
        }
    }

    public void Clear()
    {
        isClear = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRoom = true;
            doorAnimator.SetTrigger("DoorToggle");
            monsterRoom.PlayerInRoom();
        }
    }
}
