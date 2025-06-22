using System;
using UnityEngine;

public class MonsterRoom : MonoBehaviour
{
    [SerializeField] protected bool isInRoom = false; // 플레이어가 방에 있는지 여부
    [SerializeField] protected MasterRoom masterRoom; // 자기가 연동된 방 클래스 (클리어 전달 용)
    public enum WorkResult // 작업 결과 이넘
    {
        Good,
        SoSo,
        Bad
    }

    public enum WorkType // 작업 타입 이넘
    {
        Instinct, // 본능
        Attachment, // 애착
        Insight, // 통찰
        Suppression // 억압
    }
    public WorkResult result; // 현재 작업 결과
    public WorkType workType; // 현재 작업 타입
    
    /// <summary>
    /// 플레이어가 방에 들어왔을때 초기화 하는 함수
    /// </summary>
    public virtual void PlayerInRoom()
    {
        isInRoom = true;
    }
    /// <summary>
    /// 플레이어 밖으로 나가면 호출되는 함수
    /// </summary>
    public virtual void PlayerOutRoom()
    {
        isInRoom = false;
    }
    /// <summary>
    /// 작업 끝나고 결과 보내는 함수
    /// </summary>
    /// <param name="result">작업 결과 함수</param>
    public virtual void Result(WorkResult result)
    {
        
    }
    /// <summary>
    /// 환상체가 탈출하는 함수
    /// </summary>
    public virtual void JailBreak()
    {
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
    }
    /// <summary>
    /// 통찰 작업
    /// </summary>
    /// <param name="value">버튼 값</param>
    public virtual void ClickButtonManual(int value)
    {

    }
}