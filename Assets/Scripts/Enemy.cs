using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackPower;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDelay;
    [SerializeField] private Workroom room;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
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

