using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : Node
{
    GlobalBlackboard globalBlackboard;

    public ShootPlayer(ref GlobalBlackboard _globalblackboard)
    {
        globalBlackboard = _globalblackboard;
    }
    public override Status Tick()
    {
        Debug.Log("Player has been shot!");
        globalBlackboard.respawn.Respawn();
        return Status.SUCCESS;
    }
}
