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
    public LayerMask panicLayer;
    
    public float moveSpeed;
    
    public float maxHp;
    public float hp;
    public float maxMental;
    public float mental;
    
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
        mental = maxMental;
        currentDelay = 0;
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
        if (targetMonster == null)
        {
            return;
        }
        else if (Vector3.Distance(transform.position, targetMonster.transform.position) < attackDistance)
        {
            agent.isStopped = true;
            attackDelay = 0;
            state = HumanState.Attack;
        }
        else
        {
            agent.SetDestination(targetMonster.transform.position);
        }
    }
    
    public void Attack()
    {
        if (Vector3.Distance(transform.position, targetMonster.transform.position) <= attackDistance)
        {
            if (currentDelay >= attackDelay)
            {
                animator.SetTrigger("toAttack");
                targetMonster.GetComponent<IDamagable>().TakeDamage(attackPower);
                currentDelay = 0;
            }
            else
            {
                currentDelay += Time.deltaTime;
            }   
        }
        else
        {
            animator.SetTrigger("toWalk");
            state = HumanState.Move;
        }
    }

    public void Die()
    {
      
    }

    public void Panic()
    {
        if (targetMonster == null)
        {
            Collider[] colliders = new Collider[5];
            int count = Physics.OverlapSphereNonAlloc(transform.position, 3f, colliders, panicLayer.value);

            GameObject closest = null;
            float minDist = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                if (colliders[i].gameObject != this.gameObject)
                {
                    float dist = Vector3.Distance(transform.position, colliders[i].transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = colliders[i].gameObject;
                    }
                }
            }

            if (closest != null)
            {
                targetMonster = closest;
                animator.SetTrigger("toWalk"); // 필요 시 애니메이션 트리거
            }
        }
        else
        {
            Vector3 runDirection = (transform.position - targetMonster.transform.position).normalized;
            Vector3 runTo = transform.position + runDirection * 5f; // 5m 거리로 도망

            agent.SetDestination(runTo);
        }
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        if (isDie) return;
        targetMonster = attacker;
        mental -= damage * 2;
        hp -= damage;
        if (hp <= 0)
        {
            GameManager.Instance.DeleteHumans(this.gameObject);
            state = HumanState.Die;
            isDie  = true;
            agent.isStopped = true;
            animator.SetTrigger("toDie");
            agent.isStopped = true;
            Destroy(gameObject,30f);
        }
        else if(mental <= 0)
        {
            state = HumanState.Panic;
            agent.isStopped = false;
            animator.SetTrigger("toWalk"); // 필요 시 애니메이션 트리거
        }
        else
        {
            if (state != HumanState.Move)
            {
                state = HumanState.Move;
                agent.isStopped = false;
                animator.SetTrigger("toWalk"); // 필요 시 애니메이션 트리거
            }
        }
    }

    public void FindMonster()
    {
        state = HumanState.Move;
        agent.isStopped = false;
        animator.SetTrigger("toWalk");
    }
    public void TakeDamage(float damage)
    {
        
    }
}
