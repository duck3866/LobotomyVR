using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterRoom : MonoBehaviour, IDamagable
{
    [SerializeField] protected bool isInRoom = false; // 플레이어가 방에 있는지 여부
    [SerializeField] protected MasterRoom masterRoom; // 자기가 연동된 방 클래스 (클리어 전달 용)
    
    [SerializeField] protected float needItemCount = 3f; // 줘야하는 아이템 개수
    [SerializeField] protected float nowItemCount = 0f;  // 현재 아이템 개수
    
    [SerializeField] protected List<Sprite> images = new List<Sprite>(); // 작업 결과 이미지 리스트
    [SerializeField] protected Image Image; // 작업 결과 이미지를 띄울 이미지 오브젝트
    
    public bool jailBreak = false; // 탈출 여부

    [SerializeField] protected int valueTest; // 통찰 작업 결과 대조 값
    
    public Vector3 startPosition; // 시작 위치
    public Vector3 startRotation; // 시작 위치
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
    
    #region FSM
    
    public enum MonsterState
    {
        Move,
        Attack,
        Die
    }
    public MonsterState state = MonsterState.Move;
    // public virtual void Update()
    // {
    //     switch (state)
    //     {
    //         case MonsterState.Move:
    //             Move();
    //             break;
    //         case MonsterState.Attack:
    //             Attack();
    //             break;
    //         case MonsterState.Damaged:
    //             Damage();
    //             break;
    //         case MonsterState.Die:
    //             Die();
    //             break;
    //     }
    // }

    public virtual void Move()
    {
            
    }

    public virtual void Attack()
    {
            
    }
    public virtual void TakeDamage(float damage,  GameObject attacker)
    {
        
    }
    public virtual void Die()
    {
            
    }
    #endregion
    public virtual void Start()
    {
        startPosition = transform.position;
        startRotation =  transform.eulerAngles;
    }
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
    /// <summary>
    /// 환상체 제압될때 호출되어 초기화 하는 함수
    /// </summary>
    public virtual void Subdued()
    {
        jailBreak = false;
        GameManager.Instance.ClearJailBreak(this);
    }
    
    /// <summary>
    /// 환상체 재소환
    /// </summary>
    public virtual void Respawn()
    {
        jailBreak = false;
        Image.sprite = images[3];
        transform.eulerAngles = startRotation;
        transform.position = startPosition;
        masterRoom.InitializeRoom();
    }
    protected IEnumerator WaitSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        Respawn();
    }
}