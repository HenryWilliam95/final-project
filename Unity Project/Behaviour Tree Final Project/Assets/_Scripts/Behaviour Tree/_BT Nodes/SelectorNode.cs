using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public SelectorNode(string name, params Node[] _children) : base(name, _children) { }

    public override void OnStart() { currentChild = 0; }
    public override void OnTerminate(Status _status) { currentChild = 0; }

    public override Status Tick()
    {
        // Loop through each child of the flow control node
        foreach (Node child in children)
        {
            Status status = child.Tick();

            // If one child is a success the selector is a success
            if(status != Status.FAILURE)
            {
                return status;
            }
        }
        // If no children return success the node is a failure
        return Status.FAILURE;
    }
}
