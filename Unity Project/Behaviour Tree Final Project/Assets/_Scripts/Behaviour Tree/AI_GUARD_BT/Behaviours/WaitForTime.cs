using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitForTime : Node
{
    Guardblackboard guardblackboard;
    NavMeshAgent navAgent;
    float time;
    
    public WaitForTime(ref Guardblackboard _guardblackboard, float _time, ref NavMeshAgent _navAgent)
    {
        guardblackboard = _guardblackboard;
        time = _time;
        guardblackboard.conversationTimer = time;
        navAgent = _navAgent;
    }

    public override Status Tick()
    {
        // Set the guard to stand with the agent they are conversing with
        if (guardblackboard.finishedConversation == false)
        {
            navAgent.isStopped = true;
            guardblackboard.SetState(Guardblackboard.GuardState.converse);
        }

        // Look at the agent
        if (guardblackboard.GetGuardState() == Guardblackboard.GuardState.converse)
        {
            guardblackboard.gameObject.transform.LookAt(guardblackboard.nearbyFriendly.transform);
        }

        // Once the conversation has finished then reset values and continue on patrol route
        if(guardblackboard.finishedConversation)
        {
            Debug.Log("Finished conversation");
            navAgent.isStopped = false;
            navAgent.SetDestination(guardblackboard.destination);

            guardblackboard.nearbyFriendly = null;
            guardblackboard.SetState(Guardblackboard.GuardState.idle);
            guardblackboard.nearbyFriendly = null;
            guardblackboard.SetTriedToConverse(true);
            guardblackboard.finishedConversation = false;

            // Reset conversation time
            if(guardblackboard.conversationTimer <= 0)
            {
                guardblackboard.conversationTimer = time;
            }

            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }
}
