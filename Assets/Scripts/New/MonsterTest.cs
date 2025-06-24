using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterTest : MonsterRoom
{
    [SerializeField] private float count = 0; // 시선이 밖으로 나간 횟수
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

    private bool isStart = false;
    private bool isAttacking = false;

    public override void Start()
    {
        startPosition = transform.position;
        startRotation =  transform.eulerAngles;
        agent = GetComponent<NavMeshAgent>();
        animator  = GetComponentInChildren<Animator>();
        agent.speed = moveSpeed;
        hp = maxHp;
    }
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
        if (!jailBreak)
        {
            if (count > 3)
            {
                JailBreak();
            }   
        }
        else
        {
            if (count > 3)
            {
                isAttacking = false;
            }
            switch (state)
            {
                case MonsterState.Idle:
                    Idle();
                    break;
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

    public override void Idle()
    {
        if (isStart)
        {
            state = MonsterState.Move;
        }
    }
    public override void Move()
    {
        if (direction == null)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            GameObject target = null;
            float distance = 0;
            foreach (var findPlayer in player)
            {
                if (Vector3.Distance(transform.position, findPlayer.transform.position) > distance)
                {
                    target = findPlayer;
                    distance = Vector3.Distance(transform.position, findPlayer.transform.position);
                }
            }
            Debug.Log("끄아아아악 "+target);
            if (target != null)
            {
                direction = target;
                // animator.SetTrigger("toRun");
            }
        }
        else
        {
            state = MonsterState.Attack;
        }
    }

    public override void Attack()
    {
        if (direction == null)
        {
            state = MonsterState.Move;
            return;
        }
        if (!isAttacking)
        {
            transform.position = new Vector3(direction.transform.position.x,transform.position.y,direction.transform.position.z);
            if (direction.TryGetComponent(out IDamagable damagable))
            {
                isAttacking = true;
                damagable.TakeDamage(100f,gameObject);
                state = MonsterState.Idle;
            }
            else
            {
                isAttacking = true;
            }
        }
    }

    public override void TakeDamage(float damage,  GameObject attacker)
    {
        hp -= damage;
        if (hp <= 0)
        {
            state = MonsterState.Die;
        }
    }

    public override void Die()
    {
        agent.isStopped = true;
        // animator.SetTrigger("toDie");
        Subdued();
        StartCoroutine(WaitSpawn(5f));
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

        if (jailBreak)
        {
            if (isStart)
            {
                count += 1;
            }
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

        if (jailBreak)
        {
            isStart = true;
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
        state = MonsterState.Move;
        agent.isStopped = false;
        count = 0;
        jailBreak = true;
        // GetComponent<MeshRenderer>().enabled = false;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak(this);
    }
    public override void Respawn()
    {
        isStart = false;
        jailBreak = false;
        direction = null;
        isAttacking = false;
        Image.sprite = images[3];
        masterRoom.InitializeRoom();
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        hp = maxHp;
        agent.speed = moveSpeed;
        animator.SetTrigger("toIdle");
    }
}
