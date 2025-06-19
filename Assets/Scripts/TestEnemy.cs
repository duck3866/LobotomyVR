using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : Enemy
{
    private Vector3 point;
    [SerializeField] private float distance;
    public override void WorkResult(Workroom.WorkResultEnum result)
    {
        if (result == Workroom.WorkResultEnum.Low)
        {
            JailBreak();
        }
    }
    public override void JailBreak()
    {
        isJailBreak = true;
        room.JailBreak();
        PositionMove();
    }
    public override void Update()
    {
        // Debug.Log($"목적지와의 거리 {Vector3.Distance(transform.position,point) }");
        if (isJailBreak)
        {
            if (Vector3.Distance(transform.position,point) < distance)
            {
                PositionMove();
            }
        }
    }

    public void PositionMove()
    {
        Debug.Log("몬스터 위치 변경 시도");
        while (true)
        {
            point = transform.position + (Random.insideUnitSphere * 5f);
            point.y = 0f;
            if (NavMesh.SamplePosition(point, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path) &&
                    path.status == NavMeshPathStatus.PathComplete)
                {
                    break;
                }
            } 
        }
        _navMeshAgent.SetDestination(point);
    }
}
