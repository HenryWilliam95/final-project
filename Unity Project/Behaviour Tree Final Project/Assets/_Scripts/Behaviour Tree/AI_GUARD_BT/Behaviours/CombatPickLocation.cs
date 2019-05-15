using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatPickLocation : Node
{
    Guardblackboard guardblackboard;
    GlobalBlackboard globalBlackboard;
    NavMeshAgent navAgent;

    public CombatPickLocation(ref Guardblackboard _guardblackboard, ref NavMeshAgent _navAgent, ref GlobalBlackboard _globalblackboard)
    {
        guardblackboard = _guardblackboard;
        globalBlackboard = _globalblackboard;
        navAgent = _navAgent;
    }

    public override Status Tick()
    {
        if(guardblackboard.GetGuardState() == Guardblackboard.GuardState.alerted)
        {
            guardblackboard.destination = globalBlackboard.playerLastSighting;
            return Status.SUCCESS;
        }

        if(guardblackboard.GetGuardState() == Guardblackboard.GuardState.combat)
        {
            guardblackboard.destination = guardblackboard.playerPosition;
            return Status.SUCCESS;
        }



        //if(guardblackboard.playerPosition != globalBlackboard.playerLastSighting) { return Status.FAILURE; }

        //if(navAgent.destination != guardblackboard.playerPosition)
        //{
        //    guardblackboard.destination = guardblackboard.playerPosition;
        //    return Status.SUCCESS;
        //}

        //if(navAgent.remainingDistance >= 0.6f) { return Status.FAILURE; }

        //if (navAgent.remainingDistance <= 0.5f)
        //{
        //    guardblackboard.destination = guardblackboard.playerPosition;
        //    return Status.SUCCESS;
        //}    
        return Status.FAILURE;
    }
}
