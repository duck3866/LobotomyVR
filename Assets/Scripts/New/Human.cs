using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour, IDamagable
{
    public enum HumanState
    {
        Idle,
        Move,
        Attack,
        Die,
        Panic
    }
    public HumanState state =  HumanState.Idle;
    
    public float moveSpeed;
    public float maxHp;
    public float hp;
    public float attackDelay;
    public float attackDistance;
    public float attackPower;
    public float currentDelay;

    public bool isDie = false;
    public GameObject bloodEffect;
    public GameObject targetMonster;
    private Animator animator;
    private NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        Initialize();
    }
    public void Initialize()
    {
        hp = maxHp;
        agent.speed = moveSpeed;
        isDie = false;
    }

    public void Update()
    {
        switch (state)
        {
            case HumanState.Idle:
                Idle();
                break;
            case HumanState.Move:
                Move();
                break;
            case HumanState.Attack:
                Attack();
                break;
            case HumanState.Die:
                Die();
                break;
            case HumanState.Panic:
                Panic();
                break;
        }
    }

    public void Idle()
    {
        
    }

    public void Move()
    {
        
    }

    public void Attack()
    {
        
    }

    public void Die()
    {
      
    }

    public void Panic()
    {
        
    }
    public void TakeDamage(float damage, GameObject attacker)
    {
        
    }

    public void FindMonster()
    {
        state = HumanState.Move;
        animator.SetTrigger("toWalk");
    }
    public void TakeDamage(float damage)
    {
        if (isDie) return;
        hp -= damage;
        if (hp <= 0)
        {
            GameManager.Instance.DeleteHumans(this.gameObject);
            state = HumanState.Die;
            isDie  = true;
            animator.SetTrigger("toDie");
            Destroy(gameObject,3f);
        }
    }
}
