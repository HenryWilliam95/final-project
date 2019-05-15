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
        target = guardblackboard.nearbyFriendly.transform;

        guardblackboard.gameObject.transform.LookAt(target);
        //guardblackboard.nearbyFriendly.GetComponent<Guardblackboard>().gameObject.transform.LookAt(guardblackboard.transform);

        return Status.SUCCESS;
    }
}
