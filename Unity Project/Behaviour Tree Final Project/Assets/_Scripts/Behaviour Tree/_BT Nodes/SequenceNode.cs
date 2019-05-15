using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public SequenceNode(string name, params Node[] _children) : base(name, _children) { }

    public override void OnStart() { currentChild = 0; }
    public override void OnTerminate(Status _status) { currentChild = 0; }

    public override Status Tick()
    {
        // Loop through each child of the flow control node
        foreach (Node child in children)
        {
            Status status = child.Tick();

            // If one child reports it's not a success the sequence is either failed or running
            if(status != Status.SUCCESS)
            {
                return status;
            }
        }
        // If all children are a success the sequence is a success
        return Status.SUCCESS;
    }
}
