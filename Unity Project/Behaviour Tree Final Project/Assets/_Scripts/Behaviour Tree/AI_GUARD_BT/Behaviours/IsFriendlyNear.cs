using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFriendlyNear : Node
{
    Guardblackboard guardBlackboard;
    GlobalBlackboard globalBlackboard;

    public IsFriendlyNear(ref Guardblackboard _guardblackboard, ref GlobalBlackboard _globalblackboard)
    {
        guardBlackboard = _guardblackboard;
        globalBlackboard = _globalblackboard;
    }

    public override Status Tick()
    {
        for (int i = 0; i < globalBlackboard.guardBlackboards.Length; i++)
        {
            // if this blackboard is i, ignore it
            if(globalBlackboard.guardBlackboards[i] == guardBlackboard) { continue; }

            // if there is another guard nearby
            if(guardBlackboard.GetDistance(globalBlackboard.guardBlackboards[i].GetPosition()) < 5f)
            {
                // Check if they are already in the conversation state
                if(globalBlackboard.guardBlackboards[i].GetGuardState() == Guardblackboard.GuardState.converse)
                {
                    // Check if the nearby guard is conversing with another agent
                    if (globalBlackboard.guardBlackboards[i].nearbyFriendly != guardBlackboard.gameObject)
                    {
                        guardBlackboard.SetTriedToConverse(true);
                        return Status.FAILURE;
                    }                   
                }
                // Assign the nearby guard
                guardBlackboard.nearbyFriendly = globalBlackboard.guardBlackboards[i].gameObject;
                return Status.SUCCESS;
            }
        }
        return Status.FAILURE;
    }
}
