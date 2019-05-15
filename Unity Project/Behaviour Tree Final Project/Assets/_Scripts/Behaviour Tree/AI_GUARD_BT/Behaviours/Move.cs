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
        destination = guardBlackboard.destination;

        if(destination == null)
        {
            Debug.Log("Agent " + navAgent.gameObject.name + " has no destination!");
            return Status.FAILURE;
        }

        if(navAgent.remainingDistance < 0.5f)
        {
            Debug.Log("Agent " + navAgent.gameObject.name + " has reached their destination");
            return Status.FAILURE;
        }

        navAgent.SetDestination(destination);

        return Status.SUCCESS;
    }
}
