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
