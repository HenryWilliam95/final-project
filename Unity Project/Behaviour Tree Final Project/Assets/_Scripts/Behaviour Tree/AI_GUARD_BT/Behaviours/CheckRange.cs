using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : Node
{
    Guardblackboard guardblackboard;
    float range;

    public CheckRange(ref Guardblackboard _guardblackboard, float _range)
    {
        guardblackboard = _guardblackboard;
        range = _range;
    }

    public override Status Tick()
    {
        if (guardblackboard.GetDistance(guardblackboard.player.transform.position) < range)
        {
            Debug.Log("Guard is in range");
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}
