namespace Codeturion.DataStructures;

public class Node
{
    public readonly int Key;

    private Node? _nextNode;
    private Node? _previousNode;

    private string _data;

    public Node(int i, string givenData)
    {
        Key = i;
        _data = givenData;
    }
    
    public void SetNext(Node? node)
    {
        _nextNode = node;
    }

    public void SetPrevious(Node? node)
    {
        _previousNode = node;
    }

    public Node? GetPrevious()
    {
        return _previousNode;
    }

    public Node? GetNext()
    {
        return _nextNode;
    }
}