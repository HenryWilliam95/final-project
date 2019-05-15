using System.Collections.Generic;

public class CompositeNode : Node
{
    protected List<Node> children = new List<Node>();
    protected int currentChild;

    public string nodeName;

    public List<Node> GetChildren() { return children; }

    public CompositeNode(string name, params Node[] _children)
    {
        nodeName = name;
        children.AddRange(_children);
    }
}

