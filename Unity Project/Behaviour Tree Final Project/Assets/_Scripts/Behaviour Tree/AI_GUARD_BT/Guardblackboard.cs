using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardblackboard : MonoBehaviour
{
    // AI States
    public enum GuardState { idle, alerted, combat, converse, investigate }
    public GuardState state;
    public void SetState(GuardState _guardState) { state = _guardState; }
    public GuardState GetGuardState() { return state; }

    // Movement
    public Vector3 destination { get; set; }
    public GameObject[] patrolLocations { get; set; }

    // Conversations 
    const float CONVERSATION_RETRY_TIMER = 10f;
    public float converseRetryTimer = 0f;
    public float conversationTimer = 0f;
    public bool triedToConverse { get; set; }
    public bool finishedConversation { get; set; }
    public GameObject nearbyFriendly;

    public Vector3 GetPosition() { return transform.position; }
    public float GetDistance(Vector3 _target) { return Vector3.Distance(transform.position, _target); }

    public bool playerInSight { get; set; }
    public Vector3 lastPlayerSighting;
    public Vector3 playerPosition;
    public float alertTimer = 10f;

    public float investigateTimer = 15f;

    GlobalBlackboard globalBlackboard;

    private void OnValidate()
    {
        globalBlackboard = FindObjectOfType<GlobalBlackboard>();
        patrolLocations = GameObject.FindGameObjectsWithTag(gameObject.name + " Waypoints");
        triedToConverse = false;
    }

    private void Update()
    {
        if(GetGuardState() == GuardState.converse)
        {
            conversationTimer -= Time.deltaTime;

            if (conversationTimer <= 0)
            {
                finishedConversation = true;
            }
        }

        if(triedToConverse)
        {
            ConversationTime();
        }

        if(GetGuardState() == GuardState.alerted)
        {
            alertTimer -= Time.deltaTime;

            if(alertTimer <= 0)
            {
                SetState(GuardState.investigate);
                Debug.Log("Guards are returning to an alerted patrol");
                alertTimer = 10f;
            }
        }

        if(GetGuardState() == GuardState.investigate)
        {
            investigateTimer -= Time.deltaTime;

            if (investigateTimer <= 0)
            {
                SetState(GuardState.idle);
                Debug.Log("Guards are returning to their idle patrol");
                investigateTimer = 10f;
            }
        }

        lastPlayerSighting = globalBlackboard.playerLastSighting;
    }

    public void SetTriedToConverse(bool conversation)
    {
        triedToConverse = conversation;

        if (triedToConverse == true)
        {
            converseRetryTimer = CONVERSATION_RETRY_TIMER;
        }
    }

    public void ConversationTime()
    {
        if (converseRetryTimer <= 0)
        {
            triedToConverse = false;
            converseRetryTimer = CONVERSATION_RETRY_TIMER;
            return;
        }

        converseRetryTimer -= Time.deltaTime;
    }
}
