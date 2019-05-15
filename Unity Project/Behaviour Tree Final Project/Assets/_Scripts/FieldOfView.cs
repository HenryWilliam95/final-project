using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    float fieldOFViewAngle = 110f; // Angle of view from Vector3.Forward

    public GameObject player;
    Vector3 lastKnownPosition;

    SphereCollider detectionZone;

    //GuardBlackboard guardBlackboard
    //Globalblackboard globalBlackboard

    private void Awake()
    {
        detectionZone = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {;
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            Vector3 vectorToPlayer = other.transform.position - transform.position;
            float angle = Vector3.Angle(vectorToPlayer, transform.forward);

            if(angle < fieldOFViewAngle)
            {
                RaycastHit hit;

                // Exclude enemy layer
                int layerMask = 1 << 10;
                layerMask = ~layerMask;

                if(Physics.Raycast(transform.position + Vector3.up, vectorToPlayer, out hit, detectionZone.radius, layerMask))
                {
                    Debug.Log("Field of view raycast has hit: " + hit.collider.name);   // Check what has been hit
                    Debug.DrawRay(transform.position + Vector3.up, vectorToPlayer, Color.green);    // Draw a line towards the player

                    if(hit.collider.CompareTag("Player"))
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
