using System;
using UnityEngine;

public class MonsterRoom : MonoBehaviour
{
    [SerializeField] private bool isInRoom = false;
    [SerializeField] private float count = 0;
    public void PlayerInRoom()
    {
        isInRoom = true;
        count = 0f;
    }

    public void Update()
    {
        if (count > 3)
        {
            Debug.Log("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");
        }
    }

    private void OnBecameInvisible()
    {
        if (isInRoom)
        {
            Debug.Log("OnBecameInvisible 호출됨");
            count += 1;
        }
    }

    private void OnBecameVisible()
    {
        if (isInRoom)
        {
            Debug.Log("OnBecameVisible 호출됨");
        }
    }
}