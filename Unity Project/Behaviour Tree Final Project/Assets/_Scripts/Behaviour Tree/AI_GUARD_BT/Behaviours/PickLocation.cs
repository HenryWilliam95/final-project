using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickLocation : Node
{
    Guardblackboard guardBlackboard;
    NavMeshAgent navAgent;
    GameObject character;

    public GameObject[] patrolLocations;
    int destination;

    public PickLocation(ref Guardblackboard _guardblackboard, ref NavMeshAgent _navAgent, GameObject _character)
    {
        guardBlackboard = _guardblackboard;
        navAgent = _navAgent;
        character = _character;

        patrolLocations = guardBlackboard.patrolLocations;

        destination = Random.Range(0, patrolLocations.Length);
        guardBlackboard.destination = patrolLocations[destination].transform.position;
    }

    public override Status Tick()
    {
        if (patrolLocations == null)
        {
            Debug.Log("Agent " + character.name + " has no valid patrol positions");
            return Status.FAILURE;
        }

        if (navAgent.remainingDistance >= 0.5) { return Status.FAILURE; }

        if(navAgent.remainingDistance <= 0.5f)
        {
            destination = PickNewLocation();
            guardBlackboard.destination = patrolLocations[destination].transform.position;

            navAgent.SetDestination(guardBlackboard.destination);
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }

    int PickNewLocation()
    {
        int newDestination = Random.Range(0, patrolLocations.Length);

        // Ensure that agent doesn't select the same waypoint
        if (newDestination == destination) { return PickNewLocation(); }

        return newDestination;
    }
}
