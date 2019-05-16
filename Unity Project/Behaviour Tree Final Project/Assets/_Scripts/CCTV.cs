using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    GlobalBlackboard globalBlackboard;
    GameObject player;

    public bool playerInSight;
    public bool playerRecentlySpotted;
    public float timeSincePlayerLeft = 0;

    // Start is called before the first frame update
    void Awake()
    {
        globalBlackboard = FindObjectOfType<GlobalBlackboard>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // If the player has been spotted recently perform a countdown to "stop" camera seeing player.
        if(playerRecentlySpotted)
        {
            timeSincePlayerLeft += Time.deltaTime;

            if(timeSincePlayerLeft >= 10f)
            {
                playerInSight = false;
                playerRecentlySpotted = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;

            // Find the player's position and create a raycast to make sure camera can see player and they're not behind a wall
            Vector3 relativePlayerPosition = player.transform.position - transform.position;
            RaycastHit hit;

            if(Physics.Raycast(transform.position, relativePlayerPosition, out hit))
            {
                if(hit.collider.gameObject == player)
                {
                    Debug.Log("Camera spotted player");
                    playerInSight = true;
                    globalBlackboard.playerLastSighting = player.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerRecentlySpotted = true;
        }
    }
}
