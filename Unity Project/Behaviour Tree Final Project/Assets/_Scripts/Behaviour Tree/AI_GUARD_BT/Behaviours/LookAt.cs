using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : Node
{
    Guardblackboard guardblackboard;
    Transform target;

    public LookAt(ref Guardblackboard _guardblackboard)
    {
        guardblackboard = _guardblackboard;
        
    }

    public override Status Tick()
    {
        // Set the look at target for the guard to the guard they are conversing with
        target = guardblackboard.nearbyFriendly.transform;

        guardblackboard.gameObject.transform.LookAt(target);
        return Status.SUCCESS;
    }
}
