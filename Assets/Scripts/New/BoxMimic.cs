using System;
using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using UnityEngine;

public class BoxMimic : MonsterRoom
{
    private Mimic mimic;
    private Movement _movement;

    public override void Start()
    {
        mimic = GetComponent<Mimic>();
        _movement = GetComponent<Movement>();
    }

    public void Update()
    {

    }
    public override void JailBreak()
    {
        jailBreak = true;
        OnClickDontTouchButton();
        state = MonsterState.Move;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak(this);
    }
    public void OnClickDontTouchButton()
    {
        mimic.enabled = true;
        _movement.enabled = true;
    }

    public void Die()
    {
        mimic.enabled = false;
        _movement.enabled = false;
    }
}
