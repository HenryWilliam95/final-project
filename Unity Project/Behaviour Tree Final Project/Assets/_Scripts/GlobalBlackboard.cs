using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboard : MonoBehaviour
{
    // Keep track of player position, if last sighting is equal to reset, player is hidden
    public Vector3 playerLastSighting = new Vector3(10000,10000,10000);
    public Vector3 resetPlayerSighting = new Vector3(10000, 10000, 10000);
    public RespawnPlayer respawn;

    // Store an array of all the guards and CCTV cameras
    public Guardblackboard[] guardBlackboards;
    public CCTV[] CCTVs;

    private void OnValidate()
    {
        respawn = FindObjectOfType<RespawnPlayer>();
        guardBlackboards = FindObjectsOfType<Guardblackboard>();
        CCTVs = FindObjectsOfType<CCTV>();
        playerLastSighting = resetPlayerSighting;
    }

    private void Update()
    {
        // if player has been shot
        if(respawn.playerRespawning)
        {
            // Loop through all the guards, and reset back to idle
            foreach (Guardblackboard guardblackboard in guardBlackboards)
            {
                Debug.Log("Setting guards to idle");
                foreach (CCTV camera in CCTVs)
                {
                    camera.playerInSight = false;
                    camera.playerRecentlySpotted = false;
                }

                guardblackboard.playerInSight = false;
                guardblackboard.lastPlayerSighting = resetPlayerSighting;
                playerLastSighting = resetPlayerSighting;
               
                guardblackboard.SetState(Guardblackboard.GuardState.idle);    
            }
            respawn.playerRespawning = false;
        }

        // Loop through all the guards, if one can see the player alert all other guards to the player location
        foreach (Guardblackboard guardblackboard in guardBlackboards)
        {
            if(guardblackboard.playerInSight)
            {
                foreach (Guardblackboard gb in guardBlackboards)
                {
                    // If the guard can already see the player, ignore them
                    if (gb.GetGuardState() == Guardblackboard.GuardState.combat) { continue; }

                    guardblackboard.destination = playerLastSighting;

                    // Set all guards to the alerted state;
                    gb.SetState(Guardblackboard.GuardState.alerted);
                }
                return;
            }
        }

        // Loop through all the cameras, if one can see the player alert all other guards to the player location
        foreach (CCTV camera in CCTVs)
        {
            if(camera.playerInSight)
            {
                foreach (Guardblackboard guardblackboard in guardBlackboards)
                {
                    // If the guard can already see the player, ignore them
                    if (guardblackboard.GetGuardState() == Guardblackboard.GuardState.combat) { continue; }

                    guardblackboard.destination = playerLastSighting;

                    // Set all guards to the alerted state;
                    guardblackboard.SetState(Guardblackboard.GuardState.alerted);
                }
                return;
            }
        }
    }
}
