namespace Codeturion.DataStructures;

public class Node<T>
{
    public readonly int Key;

    private Node<T>? _nextNode;
    private Node<T>? _previousNode;

    public T Data;

    public Node(int i, T givenData)
    {
        Key = i;
        Data = givenData;
    }
    
    public void SetNext(Node<T>? node)
    {
        _nextNode = node;
    }

    public void SetPrevious(Node<T>? node)
    {
        _previousNode = node;
    }

    public Node<T>? GetPrevious()
    {
        return _previousNode;
    }

    public Node<T>? GetNext()
    {
        return _nextNode;
    }
}
