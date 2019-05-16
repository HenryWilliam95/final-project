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
        // If the guard has been notified of player location, move to last sighting
        if(guardblackboard.GetGuardState() == Guardblackboard.GuardState.alerted)
        {
            guardblackboard.destination = globalBlackboard.playerLastSighting;
            return Status.SUCCESS;
        }

        // If guard can see the player, move towards the player
        if(guardblackboard.GetGuardState() == Guardblackboard.GuardState.combat)
        {
            guardblackboard.destination = guardblackboard.playerPosition;
            return Status.SUCCESS;
        } 
        return Status.FAILURE;
    }
}
