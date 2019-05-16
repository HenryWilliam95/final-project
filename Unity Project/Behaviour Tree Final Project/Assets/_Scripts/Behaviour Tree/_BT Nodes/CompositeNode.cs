using System.Collections.Generic;

public class CompositeNode : Node
{
    // Create a list to store all children nodes
    protected List<Node> children = new List<Node>();
    protected int currentChild;

    // Give the node a name to easily identify for debugging
    public string nodeName;

    // Return a list of children
    public List<Node> GetChildren() { return children; }

    public CompositeNode(string name, params Node[] _children)
    {
        nodeName = name;
        children.AddRange(_children);
    }
}

