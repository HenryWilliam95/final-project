﻿using System.Collections;
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

        // Initialise the tree
        aiRoot = InitializeBehaviourTree();
    }

    private void Update()
    {
        // Each update recheck the tree from the root.  This could be optimised to check at set intervals rather than each frame
        // Could also perform more optimisation by returning to nodes that return "RUNNING" rather than recheck the tree
        aiRoot.Tick();
    }

    Node InitializeBehaviourTree()
    {
        #region IDLE BEHAVIOURS
        // Selector to decide if the guard should selector a new waypoint on continue on his current path
        SelectorNode movement = new SelectorNode("Movement Selector",
            new PickLocation(ref guardBlackboard, ref navAgent, gameObject),
            new Move(ref guardBlackboard, ref navAgent));

        // Checks the guards state, if idle, set speed and perform movement selector
        SequenceNode idlePatrol = new SequenceNode("Idle Patrol",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.idle, ref navAgent),
            new SetSpeed(IDLE_SPEED, ref navAgent),
            movement);

        // Checks the guards state, if investigating, set speed and patrol waypoints at increased speed
        SequenceNode alertedPatrol = new SequenceNode("Alerted Patrol",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.investigate, ref navAgent),
            new SetSpeed(ALERTED_SPEED, ref navAgent),
            movement);

        // Checks state, if idle or conversing then condition is passed
        SelectorNode conversationStateCheck = new SelectorNode("Conversation State Check",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.idle, ref navAgent),
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.converse, ref navAgent));

        // If conversation check is passed try and perform conversation branch
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
        // Movement is a sequence rather than selector like idle as player could be moving
        SequenceNode combatMovement = new SequenceNode("Combat Movement",
            new CombatPickLocation(ref guardBlackboard, ref navAgent, ref globalBlackboard),
            new Move(ref guardBlackboard, ref navAgent));

        SequenceNode shootPlayer = new SequenceNode("Shoot Player",
            new CheckRange(ref guardBlackboard, 3f),
            new ShootPlayer(ref globalBlackboard));

        SelectorNode shootOrMove = new SelectorNode("Shoot or Move",
            shootPlayer,
            combatMovement);

        // If the player has been sighted by another guard or by the CCTV move to the spoted location and perform a "wander"
        // type steering behaviour
        SequenceNode playerSighted = new SequenceNode("Player Sighted",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.alerted, ref navAgent),
            new SetSpeed(ALERTED_SPEED, ref navAgent),
            new SetSearchZone(ref guardBlackboard, ref globalBlackboard, ref navAgent, 10));

        // If the player is in sight of this guard, set speed and close them down
        SequenceNode playerInSight = new SequenceNode("Player In Sight",
            new CheckState(ref guardBlackboard, Guardblackboard.GuardState.combat, ref navAgent),
            new SetSpeed(COMBAT_SPEED, ref navAgent),
            shootOrMove);

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
