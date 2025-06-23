using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterTest2 : MonsterRoom
{
    public float chaseDistance;
    public float attackDistance;
    public float attackDelay;
    private float nowAttackDelay;
    public float attackPower;
    public float maxHp;
    public float hp;
    public float moveSpeed;
    public GameObject direction;
    private NavMeshAgent agent;
    private Animator animator;

    public override void Start()
    {
        startPosition = transform.position;
        startRotation =  transform.eulerAngles;
        agent = GetComponent<NavMeshAgent>();
        animator  = GetComponentInChildren<Animator>();
        agent.speed = moveSpeed;
        hp = maxHp;
    }
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            JailBreak();
        }

        if (Input.GetMouseButtonDown(1))
        {
            TakeDamage(5f,this.gameObject);
        }
        if (jailBreak)
        {
            // Debug.Log("jailBreak");
            switch (state)
            {
                case MonsterState.Move:
                    Move();
                    break;
                case MonsterState.Attack:
                    Attack();
                    break;
                case MonsterState.Die:
                    Die();
                    break;
            }
        }
    }

    public override void Move()
    {
        if (direction == null)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            GameObject target = null;
            float distance = float.MaxValue;
            foreach (var findPlayer in player)
            {
                if (Vector3.Distance(transform.position, findPlayer.transform.position) < distance)
                {
                    target = findPlayer;
                    distance = Vector3.Distance(transform.position, findPlayer.transform.position);
                }
            }
            Debug.Log("끄아아아악"+target);
            if (target != null)
            {
                direction = target;
                animator.SetTrigger("toRun");
            }
        }
        else
        {
            // if (Vector3.Distance(transform.position,direction.transform.position) > chaseDistance)
            // {
            //     direction = null;
            //     return;
            // }
            if (Vector3.Distance(transform.position,direction.transform.position) > attackDistance)
            {
                agent.SetDestination(direction.transform.position);   
            }
            else
            {
                state = MonsterState.Attack;
            }
        }
    }

    public override void Attack()
    {
        if (direction == null)
        {
            state = MonsterState.Move;
            return;
        }
        if (Vector3.Distance(transform.position,direction.transform.position) <= attackDistance)
        {
            if (nowAttackDelay >= attackDelay)
            {
                animator.SetTrigger("toAttack");
                direction.GetComponent<IDamagable>().TakeDamage(attackPower);
                nowAttackDelay = 0;
            }
            else
            {
                nowAttackDelay += Time.deltaTime;
            }   
        }
        else
        {
            state = MonsterState.Move;
            animator.SetTrigger("toRun");
            nowAttackDelay = 0;
        }
    }

    public override void TakeDamage(float damage,  GameObject attacker)
    {
        if (attacker != direction)
        {
            direction = attacker;
        }

        hp -= damage;
        if (hp <= 0)
        {
            state = MonsterState.Die;
        }
    }

    public override void Die()
    {
        agent.isStopped = true;
        animator.SetTrigger("toDie");
        Subdued();
        StartCoroutine(WaitSpawn(5f));
    }
    
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
        state = MonsterState.Move;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak(this);
        agent.isStopped = false;
    }
    public override void Respawn()
    {
        jailBreak = false;
        direction = null;
        Image.sprite = images[3];
        masterRoom.InitializeRoom();
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        hp = maxHp;
        agent.speed = moveSpeed;
        animator.SetTrigger("toIdle");
    }
}
