using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetSearchZone : Node
{
    Guardblackboard guardblackboard;
    GlobalBlackboard globalBlackboard;
    NavMeshAgent navAgent;
    float searchRadius;

    public SetSearchZone(ref Guardblackboard _guardblackboard, ref GlobalBlackboard _globalblackboard, ref NavMeshAgent _navAgent, float _radius)
    {
        guardblackboard = _guardblackboard;
        globalBlackboard = _globalblackboard;
        navAgent = _navAgent;
        searchRadius = _radius;
    }

    public override Status Tick()
    {
        if (!hasReachedDestination()) { return Status.FAILURE; }

        navAgent.SetDestination(RandomPosition(globalBlackboard.playerLastSighting, searchRadius));
        return Status.SUCCESS;
    }

    bool hasReachedDestination() { return navAgent.remainingDistance <= navAgent.stoppingDistance; }

    public static Vector3 RandomPosition(Vector3 playerLastSighting, float _searchRadius)
    {
        float radius = _searchRadius;

        var randomDirection = Random.insideUnitSphere * radius;

        randomDirection += playerLastSighting;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, radius, -1);

        return navHit.position;
    }
}
