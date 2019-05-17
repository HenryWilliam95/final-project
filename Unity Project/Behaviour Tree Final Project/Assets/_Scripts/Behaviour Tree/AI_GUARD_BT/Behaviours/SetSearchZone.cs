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
        // If the agent hits a wall, to prevent standing still reset their position
        if (navAgent.velocity.magnitude <= 0.4f)
        {
            navAgent.SetDestination(RandomPosition(globalBlackboard.playerLastSighting, searchRadius));
            return Status.SUCCESS;
        }

        // If the guard hasn't reached their destination, continue on path
        if (!hasReachedDestination()) { return Status.FAILURE; }

        // If agent is at destination, set a new point within the wander zone
        navAgent.SetDestination(RandomPosition(globalBlackboard.playerLastSighting, searchRadius));
        return Status.SUCCESS;
    }

    bool hasReachedDestination() { return navAgent.remainingDistance <= navAgent.stoppingDistance; }

    // Create a random point within a radius, originating from the player's last known position
    public static Vector3 RandomPosition(Vector3 playerLastSighting, float _searchRadius)
    {
        float radius = _searchRadius;

        var randomDirection = Random.insideUnitSphere * radius;

        randomDirection += playerLastSighting;

        NavMeshHit navHit;

        // Check that the position is valid
        NavMesh.SamplePosition(randomDirection, out navHit, radius, -1);

        return navHit.position;
    }
}
