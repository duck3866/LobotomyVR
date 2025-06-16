using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour, IRayInteraction
{
    public void RayInteract()
    {
        Debug.Log($"Room name : {gameObject.name}");
    }
}
