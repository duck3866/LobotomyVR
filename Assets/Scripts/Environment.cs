using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour, IRayInteraction
{
    public bool RayInteract()
    {
        Debug.Log($"Room name : {gameObject.name}");
        return false;
    }

    public void MoveCharacter(GameObject point)
    {
        
    }
}
