using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboard : MonoBehaviour
{
    public Vector3 playerLastSighting = new Vector3(10000,10000,10000);
    public Vector3 resetPlayerSighting = new Vector3(10000, 10000, 10000);

    public Guardblackboard[] guardBlackboards;
    public CCTV[] CCTVs;

    private void OnValidate()
    {
        guardBlackboards = FindObjectsOfType<Guardblackboard>();
        CCTVs = FindObjectsOfType<CCTV>();
        playerLastSighting = resetPlayerSighting;
    }

    private void Update()
    {
        foreach (Guardblackboard guardblackboard in guardBlackboards)
        {
            if(guardblackboard.playerInSight)
            {
                foreach (Guardblackboard gb in guardBlackboards)
                {
                    // If the guard can already see the player, ignore them
                    if (gb.GetGuardState() == Guardblackboard.GuardState.combat) { continue; }

                    // Set all guards to the alerted state;
                    gb.SetState(Guardblackboard.GuardState.alerted);
                }
                return;
            }
        }

        foreach (CCTV camera in CCTVs)
        {
            if(camera.playerInSight)
            {
                foreach (Guardblackboard guardblackboard in guardBlackboards)
                {
                    // If the guard can already see the player, ignore them
                    if (guardblackboard.GetGuardState() == Guardblackboard.GuardState.combat) { continue; }

                    // Set all guards to the alerted state;
                    guardblackboard.SetState(Guardblackboard.GuardState.alerted);
                }
                return;
            }
        }
    }
}
