using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckState : Node
{
    Guardblackboard guardBlackboard;
    Guardblackboard.GuardState guardState;
    NavMeshAgent navAgent;

    public CheckState(ref Guardblackboard _guardblackboard, Guardblackboard.GuardState _guardState, ref NavMeshAgent _navAgent)
    {
        guardBlackboard = _guardblackboard;
        guardState = _guardState;
        
        navAgent = _navAgent;
    }

    public override Status Tick()
    {
        // Check that the guards state.  Used as a condintional to control which behaviours agent uses depending
        // on the guards current state
        if (guardBlackboard.GetGuardState() == guardState)
        {          
            navAgent.isStopped = false;
            return Status.SUCCESS;
        }

        // If the state is not passed stop the agent
        navAgent.isStopped = true;
        return Status.FAILURE;
    }
}
