using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterTest : MonsterRoom
{
    [SerializeField] private float count = 0; // 시선이 밖으로 나간 횟수
    [SerializeField] private GameObject parent;
    public float attackPower;
    public float maxHp;
    public float hp;
    
    public GameObject direction;
   
    private Animator animator;

    private bool test = false;

    public override void Start()
    {
        startPosition = parent.transform.position;
        startRotation =  parent.transform.eulerAngles;
        
        animator  = GetComponentInChildren<Animator>();
        
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
    }

    private IEnumerator WaitAttack()
    {
        animator.SetTrigger("toAttack");
        yield return new WaitForSeconds(0.9f);
        Move();
    }
    private IEnumerator WaitLook()
    {
        yield return new WaitForSeconds(3f);
        test = true;
    }
    public override void Move()
    {
        if (hp <= 0) return;
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
                parent.transform.position = new Vector3(direction.transform.position.x,1,direction.transform.position.z);
                if (direction.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(100f,gameObject);
                }
                animator.SetTrigger("toIdle");
                direction = null;
            }
        }
    }
    

    public override void TakeDamage(float damage,  GameObject attacker)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        animator.SetTrigger("toDie");
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

        if (jailBreak && test)
        {
            Move();
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
            StartCoroutine(WaitLook());
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
        count = 0;
        test = false;
        jailBreak = true;
        // GetComponent<MeshRenderer>().enabled = false;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak(this);
        StartCoroutine(WaitAttack());
    }
    public override void Respawn()
    {
        jailBreak = false;
        direction = null;
        Image.sprite = images[3];
        masterRoom.InitializeRoom();
        parent.transform.position = startPosition;
        parent.transform.eulerAngles = startRotation;
        hp = maxHp;
        animator.SetTrigger("toIdle");
    }
}
