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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSight = false;

            // If the player enters the guards detection zone, direction towards the player and convert to an angle
            Vector3 vectorToPlayer = other.transform.position - transform.position;
            float angle = Vector3.Angle(vectorToPlayer, transform.forward);

            // If the angle is less that half the guards view angle player is within the guards LoS
            if(angle < fieldOFViewAngle * 0.5f)
            {
                RaycastHit hit;

                // Exclude enemy layer
                int layerMask = 1 << 10;
                layerMask = ~layerMask;

                // Create a raycast to ensure that the player is visible and not hidden behind an object or in other room
                if(Physics.Raycast(transform.position + Vector3.up, vectorToPlayer.normalized, out hit, detectionZone.radius, layerMask))
                {
                    Debug.DrawRay(transform.position + Vector3.up, vectorToPlayer, Color.green);    // Draw a line towards the player

                    // If the player has been seen, update relevant systems
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
        // If the guard loses the player, set them to alerted
        if(other.gameObject == player)
        {
            playerInSight = false;
            guardBlackboard.playerInSight = false;

            guardBlackboard.SetState(Guardblackboard.GuardState.alerted);
        }
    }
}
