using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    float fieldOFViewAngle = 110f; // Angle of view from Vector3.Forward
    public bool playerInSight;
    Vector3 personalLastSighting;
    Vector3 previousSighting;

    public GameObject player;
    
    SphereCollider detectionZone;

    Guardblackboard guardBlackboard;
    GlobalBlackboard globalBlackboard;

    private void Awake()
    {
        detectionZone = GetComponent<SphereCollider>();
        guardBlackboard = GetComponent<Guardblackboard>();
        globalBlackboard = FindObjectOfType<GlobalBlackboard>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(globalBlackboard.playerLastSighting != previousSighting)
        {
            personalLastSighting = globalBlackboard.playerLastSighting;
        }

        previousSighting = globalBlackboard.playerLastSighting;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSight = false;

            Debug.Log("Hit player");
            Vector3 vectorToPlayer = other.transform.position - transform.position;
            float angle = Vector3.Angle(vectorToPlayer, transform.forward);

            if(angle < fieldOFViewAngle * 0.5f)
            {
                RaycastHit hit;

                // Exclude enemy layer
                int layerMask = 1 << 10;
                layerMask = ~layerMask;

                if(Physics.Raycast(transform.position + Vector3.up, vectorToPlayer.normalized, out hit, detectionZone.radius, layerMask))
                {
                    Debug.Log("Field of view raycast has hit: " + hit.collider.name);   // Check what has been hit
                    Debug.DrawRay(transform.position + Vector3.up, vectorToPlayer, Color.green);    // Draw a line towards the player

                    if(hit.collider.CompareTag("Player"))
                    {
                        playerInSight = true;
                        guardBlackboard.playerInSight = true;

                        globalBlackboard.playerLastSighting = player.transform.position;
                        guardBlackboard.playerPosition = player.transform.position;

                        guardBlackboard.SetState(Guardblackboard.GuardState.combat);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;
            guardBlackboard.playerInSight = false;

            guardBlackboard.SetState(Guardblackboard.GuardState.alerted);
        }
    }
}
