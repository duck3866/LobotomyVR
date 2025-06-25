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

    public HumanState state = HumanState.Idle;
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
        // 대기 상태 처리 (필요시 애니메이션 등)
    }

    public void Move()
    {
        if (targetMonster == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, targetMonster.transform.position) < attackDistance)
        {
            agent.isStopped = true;
            state = HumanState.Attack;
            currentDelay = attackDelay; // 즉시 공격 가능하게
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(targetMonster.transform.position);
        }
    }

    public void Attack()
    {
        if (targetMonster == null) return;

        float distance = Vector3.Distance(transform.position, targetMonster.transform.position);

        if (distance <= attackDistance)
        {
            agent.isStopped = true;

            if (currentDelay >= attackDelay)
            {
                Vector3 direction = (targetMonster.transform.position - transform.position).normalized;
                transform.forward = direction;
                animator.SetTrigger("toAttack");
                targetMonster.GetComponent<IDamagable>().TakeDamage(attackPower,gameObject);
                Debug.Log("딜이 안드가잖아");
                currentDelay = 0f;
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
        // 죽음 상태 처리
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
                animator.SetTrigger("toWalk");
            }
        }
        else
        {
            Vector3 runDirection = (transform.position - targetMonster.transform.position).normalized;
            Vector3 runTo = transform.position + runDirection * 5f;
            agent.SetDestination(runTo);
        }
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        if (isDie) return;

        hp -= damage;
        mental -= damage * 2;
        targetMonster = attacker;

        if (hp <= 0)
        {
            gameObject.tag = "Die";
            GameManager.Instance.DeleteHumans(this.gameObject);
            state = HumanState.Die;
            isDie = true;
            agent.isStopped = true;
            animator.ResetTrigger("toAttack");
            animator.SetTrigger("toDie");
            targetMonster.GetComponentInChildren<MonsterRoom>().HumanDie();
            return;
        }

        if (mental <= 0)
        {
            state = HumanState.Panic;
            agent.isStopped = false;
            animator.SetTrigger("toWalk");
            return;
        }

        float distance = Vector3.Distance(transform.position, targetMonster.transform.position);
        if (distance <= attackDistance)
        {
            state = HumanState.Attack;
            agent.isStopped = true;
            currentDelay = attackDelay;
            animator.ResetTrigger("toWalk");
            animator.SetTrigger("toAttack");
        }
        else
        {
            if (state != HumanState.Move)
            {
                state = HumanState.Move;
                agent.isStopped = false;
                animator.SetTrigger("toWalk");
            }
        }
    }

    public void FindMonster(GameObject monster)
    {
        targetMonster = monster;
        state = HumanState.Move;
        agent.isStopped = false;
        animator.SetTrigger("toWalk");
    }

    public void ResetTarget(GameObject target)
    {
        if (target == targetMonster)
        {
            targetMonster = null;
            animator.SetTrigger("toIdle");
            agent.isStopped = true;
        }
    }
    public void TakeDamage(float damage)
    {
        // IDamagable 인터페이스 구현용
    }
}
