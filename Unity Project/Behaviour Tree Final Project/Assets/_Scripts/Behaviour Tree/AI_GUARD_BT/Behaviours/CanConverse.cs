using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanConverse : Node
{
    Guardblackboard guardBlackboard;

    public CanConverse(ref Guardblackboard _guardblackboard)
    {
        guardBlackboard = _guardblackboard;
    }

    public override Status Tick()
    {
        Guardblackboard friendlyBlackboard = guardBlackboard.nearbyFriendly.GetComponent<Guardblackboard>();

        if (guardBlackboard.triedToConverse) { return Status.FAILURE; }

        return Status.SUCCESS;
    }
}
