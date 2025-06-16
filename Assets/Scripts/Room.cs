using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<GameObject> roomInObject = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!roomInObject.Contains(other.gameObject))
        {
            roomInObject.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (roomInObject.Contains(other.gameObject))
        {
            roomInObject.Remove(other.gameObject);
        }
    }
}
