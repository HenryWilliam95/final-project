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
        // If the guard has no patrol locations, return failure
        if (patrolLocations == null)
        {
            Debug.Log("Agent " + character.name + " has no valid patrol positions");
            return Status.FAILURE;
        }

        // If the guard is still travelling to their destination don't pick a new location
        if (navAgent.remainingDistance >= 0.5) { return Status.FAILURE; }

        // If agent has reached destination pick a new location
        if(navAgent.remainingDistance <= 0.5f)
        {
            destination = PickNewLocation();
            // Update guardblackboard of location so Move can use this data
            guardBlackboard.destination = patrolLocations[destination].transform.position;

            // Send the guard on their initial destination
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
