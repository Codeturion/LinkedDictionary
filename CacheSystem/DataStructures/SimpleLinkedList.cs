using Codeturion.Debug;

namespace Codeturion.DataStructures;

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
            if (currentNode.Key != null && currentNode.Key.Equals(key))
            {
                var (nextNode, previousNode) = currentNode.GetNeighborNodes();

                if (previousNode == null)
                {
                    _headNode = nextNode;
                }
                else
                {
                    previousNode.NextNode = nextNode;
                }

                if (nextNode == null)
                {
                    _tailNode = previousNode;
                }
                else
                {
                    nextNode.PreviousNode = previousNode;
                }

                currentNode.NextNode = _headNode;
                currentNode.PreviousNode = null;

                if (_headNode != null)
                {
                    _headNode.PreviousNode = currentNode;
                    _headNode = currentNode;
                }

                return currentNode.Value;
            }

            currentNode = currentNode.NextNode;
        }

        return default;
    }

    public void Put(TKey key, TValue data)
    {
        if (IsFull())
        {
            var previousOfTail = _tailNode?.PreviousNode;

            if (previousOfTail != null)
            {
                previousOfTail.NextNode = null;
            }

            _tailNode = previousOfTail;
            _currentCount--;
        }

        var cachedNode = _headNode;
        var newNode = new LinkedNode<TKey, TValue>(key, data);

        if (_headNode == null)
        {
            _tailNode = newNode;
        }

        newNode.NextNode = cachedNode;

        if (cachedNode != null)
        {
            cachedNode.PreviousNode = newNode;
        }

        _headNode = newNode;
        _currentCount++;
    }

    private bool IsFull()
    {
        return _currentCount == _limit;
    }

    public void Print()
    {
        DebugHelper.PrintNodes(_headNode, _tailNode);
    }
}