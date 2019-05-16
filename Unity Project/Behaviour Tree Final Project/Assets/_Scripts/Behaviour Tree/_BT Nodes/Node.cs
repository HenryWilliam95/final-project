using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { SUCCESS, RUNNING, FAILURE, INVALID }

public class Node
{
    // Method used to rick the tree
    public virtual Status Tick() { return Status.INVALID; }

    // Methods called at the start and finish of a node to reset values
    public virtual void OnStart() { }
    public virtual void OnTerminate(Status _status) { }

    private Status status;

    public Node()
    {
        status = Status.INVALID;
    }

    public Status RunTree()
    {
        if(status == Status.INVALID) { OnStart(); }

        // Begin ticking the tree
        status = Tick();

        if(status != Status.RUNNING) { OnTerminate(status); }

        return status;
    }

    public Status GetStatus() { return status; }
    public void SetStatus(Status _status) { status = _status; }
}
