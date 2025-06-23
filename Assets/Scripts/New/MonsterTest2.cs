using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest2 : MonsterRoom
{
    public enum MonsterState
    {
        Move,
        Attack,
        Damaged,
        Die
    }
    private MonsterState state = MonsterState.Move;
    
    /// <summary>
    /// 플레이어가 들어왔을때
    /// </summary>
    public override void PlayerInRoom()
    {
        isInRoom = true;
    }        
    /// <summary>
    /// 통찰 작업
    /// </summary>
    /// <param name="value">버튼 값</param>
    public override void ClickButtonManual(int value)
    {
        if (jailBreak) return;
        if (value == valueTest)
        {
            Result(WorkResult.Good);
        }
        else
        {
            Result(WorkResult.Bad);
        }
    }
    /// <summary>
    /// 콜라이더 범위 트리거 본능/공격 할때 호출됨
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (!jailBreak)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                Debug.Log("Item enter");
                nowItemCount += 1;
                if (nowItemCount >= needItemCount)
                {
                    Result(WorkResult.Good);
                }
            }
        }
    }
    /// <summary>
    /// 결과 전달 함수
    /// </summary>
    /// <param name="result"></param>
    public override void Result(WorkResult result)
    {
        if (masterRoom.isClear) return;
        
        masterRoom.Clear();
        if (result == WorkResult.Good)
        {
            Image.sprite = images[0];
            GameManager.Instance.AddEnergy(10);
        }
        else if (result == WorkResult.SoSo)
        {
            Image.sprite = images[1];
            GameManager.Instance.AddEnergy(5);
        }
        else
        {
            Image.sprite = images[2];
            GameManager.Instance.AddEnergy(1);
        }
    }
    /// <summary>
    /// 탈옥 함수
    /// </summary>
    public override void JailBreak()
    {
        jailBreak = true;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak(this);
    }
}
