using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour, IRayInteraction, ICreature
{
    [SerializeField] private bool isMoving = false; // 움직이고 있는지 여부
    private Animator _animator; 
    private NavMeshAgent _agent;
    private Vector3 _destination; // 목적지
    [SerializeField] private Room PreviousRoom; // 자기가 전에 있는 방
    [SerializeField] private Room CurrentRoom; // 자기가 현재 있는 방
    [SerializeField] private Room controlRoom; // 명령받은 방
    private bool _isCC = false; // CC기 걸렸는지 여부 
    [SerializeField] private bool isWork = false; // 일하고 있는지 여부
    [SerializeField] private GameObject enemy; // 추격 중인 적
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponentInChildren<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo,int.MaxValue))
            {
                MoveCharacter(hitInfo.collider.gameObject);
            }
        }
        if (isMoving)
        {
            if (Vector3.Distance(transform.position,_destination) < 0.07f)
            {
                _animator.SetTrigger("toIdle");
                isMoving = false;
                transform.position = _destination;
            }
        }
        else if (enemy != null)
        {
            _agent.SetDestination(enemy.transform.position);
        }
    }
    public virtual bool RayInteract()
    {
        if (!_isCC && !isWork)
        {
            Debug.Log("??/");
            return true;
        }
        return false;
    }

    public virtual void MoveCharacter(GameObject point)
    {
        if (!isWork)
        {
            _agent.isStopped = false;
            LeaveRoom();
            controlRoom = point.GetComponent<Workroom>();
            PreviousRoom = CurrentRoom;
            _destination = point.transform.position;
            isMoving = true;
            _animator.SetTrigger("toWalk");
            _agent.SetDestination(_destination);
        }
    }

    public void EnterRoom(GameObject room, bool isWork)
    {
        CurrentRoom = room.GetComponent<Room>();
        
        if (!isWork)
        {
            FindEnemy();
        }
    }

    public void LeaveRoom()
    {
        enemy = null;
    }

    public void StopWalking()
    {
        LeaveRoom();
        _agent.isStopped = true;
    }
    public void FindEnemy()
    {
        if (CurrentRoom != null)
        {
            if (CurrentRoom.enemyList.Count != 0)
            {
                enemy = CurrentRoom.enemyList[0].gameObject;
            }
        }
    }

    public void Damage(int damage)
    {
        
    }

    public void Work(float time)
    {
        transform.position = _destination;
        _animator.SetTrigger("toWork");
        StartCoroutine(WalkCoroutine(time));
    }

    private IEnumerator WalkCoroutine(float time)
    {
        isWork = true;
        yield return new WaitForSeconds(time);
        controlRoom.GetComponent<Workroom>().WorkResult();
        isWork = false;
        MoveCharacter(PreviousRoom.gameObject);
    }
}

