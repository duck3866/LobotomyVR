using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterRoom : MonoBehaviour
{
    public Animator doorAnimator;
    public bool playerInRoom = false;
    public bool isClear = false;
    public MonsterRoom monsterRoom;
    /// <summary>
    /// 문을 열고 닫는 함수
    /// </summary>
    public void DoorAnimation()
    {
        if (!playerInRoom || isClear)
        {
            doorAnimator.SetTrigger("DoorToggle");
        }
    }
    /// <summary>
    /// 방의 임무를 끝내고 호출하는 함수
    /// </summary>
    public void Clear()
    {
        isClear = true;
    }
    /// <summary>
    /// 방에 들어왔을때 호출되는 트리거
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRoom = true;
            isClear = false;
            doorAnimator.SetTrigger("DoorToggle");
            monsterRoom.PlayerInRoom();
        }
    }
    /// <summary>
    /// 방에 나갔을때 호출되는 트리거
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRoom = false;
            isClear = false;
            doorAnimator.SetTrigger("DoorToggle");
            monsterRoom.PlayerOutRoom();
        }
    }
}
