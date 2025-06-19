using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRayInteraction
{
    public bool RayInteract();
    public void MoveCharacter(GameObject point);
}
