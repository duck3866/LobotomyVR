using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] private bool isRightHand = false;

    private void Update()
    {
        if (isRightHand)
        {
            if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
            {
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.TryGetComponent<IRayInteraction>(out var component))
                    {
                        component.RayInteract();
                    }
                }
            }
        }

        HandMove();
    }

    private void HandMove()
    {
        if (isRightHand)
        {
            transform.position = ARAVRInput.RHandPosition;
            transform.rotation = ARAVRInput.GetRHandRotation();
        }
        else
        {
            transform.position = ARAVRInput.LHandPosition;
            transform.rotation = ARAVRInput.GetLHandRotation();
        }
    }
}
