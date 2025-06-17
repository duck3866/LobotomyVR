using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Workroom : Room
{
    [SerializeField] private float walkSpeed; // 작업 속도
    //todo: 나중에 작업 타입에 맞춰서 속도 늦쳐지도록 수정
    [SerializeField] private bool isWalking = false; 
    // 작업 중
    [SerializeField] private GameObject enemyObject; // 관리중인 환상체
    private Enemy _enemy;
    [SerializeField] private bool jailbreak = false;
    public enum WorkResultEnum
    {
        High,
        Middle,
        Low
    }
    private void Start()
    {
        _enemy = enemyObject.GetComponent<Enemy>();
        _enemy.InitRoom(this);
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (jailbreak) return;
        if (!characterList.Contains(other.gameObject) && other.gameObject.CompareTag("Player"))
        {
            characterList.Add(other.gameObject);
            if (other.TryGetComponent<ICreature>(out ICreature creature))
            {
                creature.EnterRoom(this.gameObject,true);
                characterList[0].GetComponent<Employee>().Work(walkSpeed);
                _enemy.EnterRoom();
            }
            isWalking = true;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (jailbreak) return;
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

    public void WorkResult()
    {
        // int random = Random.Range(0, 3);
        _enemy.WorkResult(WorkResultEnum.Low);
    }
    public override bool RayInteract()
    {
        if (isWalking)
        {
            return false;
        }
        isWalking = true;
        return true;
    }
}
