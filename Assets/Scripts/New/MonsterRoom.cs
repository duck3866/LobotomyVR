using System;
using UnityEngine;

public class MonsterRoom : MonoBehaviour
{
    [SerializeField] protected bool isInRoom = false;
    [SerializeField] protected MasterRoom masterRoom;
    public enum WorkResult
    {
        Good,
        SoSo,
        Bad
    }

    public enum WorkType
    {
        Instinct, // 본능
        Attachment, // 애착
        Insight, // 통찰
        Suppression // 억압
    }
    public WorkResult result;
    public WorkType workType;
    public virtual void PlayerInRoom()
    {
        isInRoom = true;
    }

    public virtual void Update()
    {
       
    }

    public virtual void PlayerOutRoom()
    {
        isInRoom = false;
    }

    public virtual void Result(WorkResult result)
    {
        
    }

    public virtual void JailBreak()
    {
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
    }
}