using Codeturion.Scripts.Data.Nodes;
using Codeturion.Scripts.Debug;

namespace Codeturion.Scripts.Data.Structures;

public class SimpleLinkedList<TKey, TValue>
{
    private LinkedNode<TKey, TValue>? _headNode;
    private LinkedNode<TKey, TValue>? _tailNode;

    private int _currentCount;
    private readonly int _limit;

    public SimpleLinkedList(int limit)
    {
        _limit = limit;
    }

    public TValue? Get(TKey key)
    {
        var currentNode = _headNode;

        while (currentNode != null)
        {
            if (currentNode.Key != null && (currentNode.Key.Equals(key)))
            {
                UpdateNode(currentNode);
                return currentNode.Value;
            }

            currentNode = currentNode.GetNext();
        }

        return default;
    }

    public void Put(TKey key, TValue data)
    {
        if (IsFull())
        {
            var previousOfTail = _tailNode?.GetPrevious();

            previousOfTail?.SetNext(null);

            _tailNode = previousOfTail;
            _currentCount--;
        }

        var cachedNode = _headNode;
        var newNode = new LinkedNode<TKey, TValue>(key, data);

        if (_headNode == null)
        {
            _tailNode = newNode;
        }

        newNode.SetNext(cachedNode);

        cachedNode?.SetPrevious(newNode);

        _headNode = newNode;
        _currentCount++;
    }

    public void Print()
    {
        DebugHelper.PrintNodes(_headNode, _tailNode);
    }
    
    private void UpdateNode(LinkedNode<TKey, TValue> node)
    {
        DeleteNode(node);
        AddToHead(node);
    }

    private void DeleteNode(LinkedNode<TKey, TValue> nodeToRemove)
    {
        var (nextNode, previousNode) = nodeToRemove.GetNeighborNodes();

        if (previousNode == null)
        {
            _headNode = nextNode;
        }
        else
        {
            previousNode.SetNext(nextNode);
        }

        if (nextNode == null)
        {
            _tailNode = previousNode;
        }
        else
        {
            nextNode.SetPrevious(previousNode);
        }
    }

    private void AddToHead(LinkedNode<TKey, TValue> newNode)
    {
        newNode.SetNext(_headNode);
        newNode.SetPrevious(null);

        _headNode?.SetPrevious(newNode);

        _headNode = newNode;

        if (_tailNode == null)
        {
            _tailNode = newNode;
        }
    }

    private bool IsFull()
    {
        return _currentCount == _limit;
    }
}