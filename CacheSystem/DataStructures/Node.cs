namespace Codeturion.DataStructures;

public class Node<TKey,TValue> : ILinkedNode
{
    public TKey Key { get; }
    public TValue? Value { get; }
    public Node<TKey,TValue>? NextNode { get; set; }
    public Node<TKey,TValue>? PreviousNode { get; set; }

    public Node(TKey key, TValue? value)
    {
        Key = key;
        Value = value;
        NextNode = null;
        PreviousNode = null;
    }

    public string KeyAsString => Key?.ToString() ?? "";
    ILinkedNode? ILinkedNode.Next => NextNode;
    ILinkedNode? ILinkedNode.Previous => PreviousNode;
}

public interface ILinkedNode
{
    string KeyAsString { get; }
    ILinkedNode? Next { get; }
    ILinkedNode? Previous { get; }
}