using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public override void WorkResult(Workroom.WorkResultEnum result)
    {
        if (result == Workroom.WorkResultEnum.Low)
        {
            JailBreak();
        }
    }
    public override void JailBreak()
    {
        
    }
}
