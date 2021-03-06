﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetSpeed : Node
{
    NavMeshAgent navAgent;
    float speed;

    public SetSpeed(float _speed, ref NavMeshAgent _navAgent)
    {
        speed = _speed;
        navAgent = _navAgent;
    }

    public override Status Tick()
    {
        // Provided their is a NavMeshAgent present, adjust the speed to fix the current decision branch
        if(navAgent == null) { return Status.FAILURE; }

        navAgent.speed = speed;

        return Status.SUCCESS;
    }
}
