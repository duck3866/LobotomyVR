using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTest : MonsterRoom
{
    [SerializeField] private float count = 0; // 시선이 밖으로 나간 횟수
 
    [SerializeField] private float needItemCount = 3f; // 줘야하는 아이템 개수
    [SerializeField] private float nowItemCount = 0f;  // 현재 아이템 개수
    
    [SerializeField] private List<Sprite> images = new List<Sprite>(); // 작업 결과 이미지 리스트
    [SerializeField] private Image Image; // 작업 결과 이미지를 띄울 이미지 오브젝트

    [SerializeField] private int valueTest; // 통찰 작업 결과 대조 값
    [SerializeField] private bool jailBreak = false; // 탈출 여부

    private Vector3 _startPosition; // 시작 위치
    /// <summary>
    /// 플레이어가 들어왔을때
    /// </summary>
    public override void PlayerInRoom()
    {
        isInRoom = true;
        // workType = WorkType.Instinct;
        count = 0f;
    }
    
    public void Update()
    {
        if (count > 3)
        {
            if (!jailBreak)
            {
                JailBreak();
            }
        }
    }
    /// <summary>
    /// 시선이 안으로 들어옴
    /// </summary>
    private void OnBecameInvisible()
    {
        if (isInRoom)
        {
            Debug.Log("OnBecameInvisible 호출됨");
            count += 1;
        }
    }
    /// <summary>
    /// 시선이 밖으로 들어옴
    /// </summary>
    private void OnBecameVisible()
    {
        if (isInRoom)
        {
            Debug.Log("OnBecameVisible 호출됨");
        }
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
    /// 콜라이더 범위 트리거
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (jailBreak) return;
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
        GetComponent<MeshRenderer>().enabled = false;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak();
    }
}
