using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject player;
    public bool playerRespawning;

    public void Respawn()
    {
        playerRespawning = true;
        player.transform.position = transform.position;   
    }
}
