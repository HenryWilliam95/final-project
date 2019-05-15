using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardblackboard : MonoBehaviour
{
    // AI States
    public enum GuardState { idle, alerted, combat, converse }
    public GuardState state;
    public void SetState(GuardState _guardState) { state = _guardState; }
    public GuardState GetGuardState() { return state; }

    // Movement
    public Vector3 destination { get; set; }
    public GameObject[] patrolLocations { get; set; }

    // Conversations 
    const float CONVERSATION_RETRY_TIMER = 10f;
    public float converseTimer = 0f;
    public bool triedToConverse { get; set; }
    public bool finishedConversation { get; set; }
    public GameObject nearbyFriendly;

    public Vector3 GetPosition() { return transform.position; }
    public float GetDistance(Vector3 _target) { return Vector3.Distance(transform.position, _target); }

    private void OnValidate()
    {
        patrolLocations = GameObject.FindGameObjectsWithTag(gameObject.name + " Waypoints");
        triedToConverse = false;
    }

    private void Update()
    {
        if(GetGuardState() == GuardState.converse)
        {
            converseTimer -= Time.deltaTime;

            if (converseTimer <= 0)
            {
                finishedConversation = true;
            }
        }

        if(triedToConverse)
        {
            ConversationTime();
        }
    }

    public void SetTriedToConverse(bool conversation)
    {
        triedToConverse = conversation;

        if (triedToConverse == true)
        {
            converseTimer = CONVERSATION_RETRY_TIMER;
        }
    }

    public void ConversationTime()
    {
        if (converseTimer <= 0)
        {
            triedToConverse = false;
            converseTimer = CONVERSATION_RETRY_TIMER;
            return;
        }

        converseTimer -= Time.deltaTime;
    }
}
