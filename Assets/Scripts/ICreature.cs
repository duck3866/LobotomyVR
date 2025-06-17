using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    public void EnterRoom(GameObject room,bool isWork);
    public void LeaveRoom();
    public void FindEnemy();
    public void Damage(int damage);
}
