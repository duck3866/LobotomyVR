using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    // [SerializeField] private bool isRightHand = false;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask characterLayerMask;
    [SerializeField] private GameObject selectCharacter;

    private void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            Ray ray = new Ray(rightHand.transform.position, rightHand.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * int.MaxValue, Color.red, 1);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, int.MaxValue, layerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent<IRayInteraction>(out var component))
                {
                    component.RayInteract();
                    if (selectCharacter != null)
                    {
                        selectCharacter.GetComponent<IRayInteraction>().MoveCharacter(hitInfo.collider.bounds.center);
                    }
                }
            }
        }

        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.LTouch))
        {
            Ray ray = new Ray(leftHand.transform.position, leftHand.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * int.MaxValue, Color.blue, 1);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, int.MaxValue, characterLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent<IRayInteraction>(out var component))
                {
                    selectCharacter = hitInfo.collider.gameObject;
                    Debug.Log("캐릭터 선택 " + hitInfo.collider.gameObject.layer);
                }
            }
            else
            {
                selectCharacter = null;
            }
        }

        HandMove();
    }

    private void HandMove()
    {
        rightHand.transform.position = ARAVRInput.RHandPosition;
        rightHand.transform.rotation = ARAVRInput.GetRHandRotation();
        leftHand.transform.position = ARAVRInput.LHandPosition;
        leftHand.transform.rotation = ARAVRInput.GetLHandRotation();
    }
}