using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GUARD_BT : MonoBehaviour
{
    Node aiRoot;

    Guardblackboard guardBlackboard;
    GlobalBlackboard globalBlackboard;
    NavMeshAgent navAgent;

    // patrol speeds
    const float IDLE_SPEED = 3.5f;
    const float ALERTED_SPEED = 6F;


    private void Awake()
    {
        guardBlackboard = GetComponent<Guardblackboard>();
        globalBlackboard = FindObjectOfType<GlobalBlackboard>();
        navAgent = GetComponent<NavMeshAgent>();



        aiRoot = InitializeBehaviourTree();
    }

    private void Update()
    {
        aiRoot.Tick();
    }

    Node InitializeBehaviourTree()
    {
        SelectorNode movement = new SelectorNode("Movement Selector",
            new PickLocation(ref guardBlackboard, ref navAgent, gameObject),
            new Move(ref guardBlackboard, ref navAgent));

        SequenceNode idlePatrol = new SequenceNode("Idle Patrol",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.idle, ref navAgent),
            new SetSpeed(IDLE_SPEED, ref navAgent),
            movement);

        SequenceNode alertedPatrol = new SequenceNode("Alerted Patrol",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.alerted, ref navAgent),
            new SetSpeed(ALERTED_SPEED, ref navAgent),
            movement);

        SelectorNode conversationStateCheck = new SelectorNode("Conversation State Check",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.idle, ref navAgent),
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.converse, ref navAgent));

        SequenceNode converse = new SequenceNode("Conversation Sequence",
            conversationStateCheck,
            new CanConverse(ref guardBlackboard),
            new IsFriendlyNear(ref guardBlackboard, ref globalBlackboard),
            new CanFriendlyConverse(ref guardBlackboard),
            new LookAt(ref guardBlackboard),
            new WaitForTime(ref guardBlackboard, 4f));

        SelectorNode patrol = new SelectorNode("Patrol Selector",
            converse,
            alertedPatrol,
            idlePatrol);

        SelectorNode idle = new SelectorNode("Idle Selector",
            //shouldConverse,
            patrol);

        SelectorNode root = new SelectorNode("root",
            idle
            //,combat
            );

        return root;
    }
}
