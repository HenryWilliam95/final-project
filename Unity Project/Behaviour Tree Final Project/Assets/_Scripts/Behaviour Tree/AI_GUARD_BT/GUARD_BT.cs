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
    const float ALERTED_SPEED = 6f;
    const float COMBAT_SPEED = 5f;


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
        #region IDLE BEHAVIOURS
        SelectorNode movement = new SelectorNode("Movement Selector",
            new PickLocation(ref guardBlackboard, ref navAgent, gameObject),
            new Move(ref guardBlackboard, ref navAgent));

        SequenceNode idlePatrol = new SequenceNode("Idle Patrol",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.idle, ref navAgent),
            new SetSpeed(IDLE_SPEED, ref navAgent),
            movement);

        SequenceNode alertedPatrol = new SequenceNode("Alerted Patrol",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.investigate, ref navAgent),
            new SetSpeed(ALERTED_SPEED, ref navAgent),
            movement);

        SelectorNode conversationStateCheck = new SelectorNode("Conversation State Check",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.idle, ref navAgent),
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.converse, ref navAgent));

        SequenceNode converse = new SequenceNode("Conversation Sequence",
            conversationStateCheck,
            new CanConverse(ref guardBlackboard),
            new IsFriendlyNear(ref guardBlackboard, ref globalBlackboard),
            //new CanFriendlyConverse(ref guardBlackboard),
            //new LookAt(ref guardBlackboard),
            new WaitForTime(ref guardBlackboard, 4f, ref navAgent));

        SelectorNode patrol = new SelectorNode("Patrol Selector",
            alertedPatrol,
            idlePatrol);

        SelectorNode idle = new SelectorNode("Idle Selector",
            converse,
            patrol);
        #endregion

        #region COMBAT BEHAVIOURS
        SequenceNode combatMovement = new SequenceNode("Combat Movement",
            new CombatPickLocation(ref guardBlackboard, ref navAgent, ref globalBlackboard),
            new Move(ref guardBlackboard, ref navAgent));

        SequenceNode playerSighted = new SequenceNode("Player Sighted",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.alerted, ref navAgent),
            new SetSpeed(ALERTED_SPEED, ref navAgent),
            new SetSearchZone(ref guardBlackboard, ref globalBlackboard, ref navAgent, 10));

        SequenceNode playerInSight = new SequenceNode("Player In Sight",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.combat, ref navAgent),
            new SetSpeed(COMBAT_SPEED, ref navAgent),
            combatMovement);

        SelectorNode combat = new SelectorNode("Combat Selector",
            playerInSight,
            playerSighted);
        #endregion

        SelectorNode root = new SelectorNode("root",
            idle,
            combat);

        return root;
    }
}
