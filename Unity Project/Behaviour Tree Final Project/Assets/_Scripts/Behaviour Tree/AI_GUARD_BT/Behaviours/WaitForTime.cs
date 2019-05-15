using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : Node
{
    Guardblackboard guardblackboard;
    float time;
    
    public WaitForTime(ref Guardblackboard _guardblackboard, float _time)
    {
        guardblackboard = _guardblackboard;
        guardblackboard.converseTimer = _time;
    }

    public override Status Tick()
    {
        if(guardblackboard.nearbyFriendly == null) { return Status.FAILURE; }
        Guardblackboard friendlyGuardblackboard = guardblackboard.nearbyFriendly.GetComponent<Guardblackboard>();

        if (guardblackboard.finishedConversation == false)
        {
            Debug.Log("Started conversation");
            guardblackboard.SetState(Guardblackboard.GuardState.converse);
            friendlyGuardblackboard.SetState(Guardblackboard.GuardState.converse);
        }

        if(guardblackboard.finishedConversation)
        {
            Debug.Log("Finished conversation");
            friendlyGuardblackboard.finishedConversation = true;
            friendlyGuardblackboard.SetState(Guardblackboard.GuardState.idle);
            friendlyGuardblackboard.nearbyFriendly = null;

            guardblackboard.finishedConversation = true;
            guardblackboard.SetState(Guardblackboard.GuardState.idle);
            guardblackboard.nearbyFriendly = null;

            return Status.SUCCESS;
        }

        return Status.FAILURE;
    }
}
