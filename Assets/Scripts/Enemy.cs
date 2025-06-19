using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float attackPower;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxHp;
    [SerializeField] protected Workroom room;
    protected bool isJailBreak = false;
    protected Animator _animator;
    protected NavMeshAgent _navMeshAgent;

    private void Start()
    {
        isJailBreak = false;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed;
    }

    public virtual void Update()
    {
        if (isJailBreak)
        {
            
        }
    }

    public virtual void InitRoom(Workroom room)
    {
        this.room = room;
    }

    public virtual void EnterRoom()
    {
        
    }

    public virtual void WorkResult(Workroom.WorkResultEnum result)
    {
        
    }
    public virtual void JailBreak()
    {
        
    }
}

