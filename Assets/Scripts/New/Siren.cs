using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour
{
    private Light Light;
    public bool SirenActive;
    public float rotationSpeed;
    public void Start()
    {
        Light = GetComponent<Light>();
        Light.enabled = false;
    }

    public void StartSiren()
    {
        SirenActive = true;
        Light.enabled = true;
    }

    public void EndSiren()
    {
        SirenActive = false;
        Light.enabled = false;
    }

    public void Update()
    {
        if (SirenActive)
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);
        }
    }
}
