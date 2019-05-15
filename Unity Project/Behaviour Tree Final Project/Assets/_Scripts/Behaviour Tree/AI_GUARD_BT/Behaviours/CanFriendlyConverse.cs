using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanFriendlyConverse : Node
{
    Guardblackboard guardBlackboard;

    public CanFriendlyConverse(ref Guardblackboard _guardBlackboard)
    {
        guardBlackboard = _guardBlackboard;
    }

    public override Status Tick()
    {
        Guardblackboard friendlyBlackboard = guardBlackboard.nearbyFriendly.GetComponent<Guardblackboard>();

        if (friendlyBlackboard.triedToConverse)
        {
            Debug.Log("Friendly cannot converse");
            guardBlackboard.nearbyFriendly = null;
            return Status.FAILURE;
        }

        Debug.Log("Friendly can converse");
        guardBlackboard.triedToConverse = true;
        friendlyBlackboard.triedToConverse = true;
        
        return Status.SUCCESS;
    }
}
