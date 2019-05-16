using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : Node
{
    Guardblackboard guardBlackboard;
    NavMeshAgent navAgent;
    Vector3 destination;

    public Move(ref Guardblackboard _guardblackboard, ref NavMeshAgent _navAgent)
    {
        guardBlackboard = _guardblackboard;
        navAgent = _navAgent;
    }

    public override Status Tick()
    {
        // Pull destination selected by PickLocation from the blackboard
        destination = guardBlackboard.destination;

        // If the destination is null return failure
        if(destination == null)
        {
            Debug.Log("Agent " + navAgent.gameObject.name + " has no destination!");
            return Status.FAILURE;
        }

        // If agent has not reached destination, return failure
        if(navAgent.remainingDistance < 0.5f)
        {
            Debug.Log("Agent " + navAgent.gameObject.name + " has reached their destination");
            return Status.FAILURE;
        }

        // Set the destination
        navAgent.SetDestination(destination);

        return Status.SUCCESS;
    }
}
