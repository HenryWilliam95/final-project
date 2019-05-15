using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertedWander : Node
{
    GlobalBlackboard globalBlackboard;
    Guardblackboard guardblackboard;
    NavMeshAgent navAgent;
    float searchRadius;

    public AlertedWander(ref GlobalBlackboard _globalblackboard, ref Guardblackboard _guardblackboard, ref NavMeshAgent _navAgent, float _searchRadius)
    {
        globalBlackboard = _globalblackboard;
        guardblackboard = _guardblackboard;
        navAgent = _navAgent;
        searchRadius = _searchRadius;
    }

    public override Status Tick()
    {
        navAgent.SetDestination(SetSearchZone.RandomPosition(globalBlackboard.playerLastSighting, searchRadius));
        return Status.SUCCESS;
    }
}
