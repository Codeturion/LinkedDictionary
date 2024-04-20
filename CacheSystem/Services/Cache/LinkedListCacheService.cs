using System.Collections;
using Codeturion.DataStructures;
using Codeturion.Debug;

namespace Codeturion.Services.Cache
{
    public class LinkedListCacheService<TKey, TValue> : ICacheService<TKey, TValue>, IEnumerable
    {
        private int _currentCount;
        private Node<TKey, TValue>? _headNode;
        private Node<TKey, TValue>? _tailNode;
        private readonly int _limit;

        public LinkedListCacheService(int limit)
        {
            _limit = limit;
        }

        public IEnumerator GetEnumerator()
        {
            var currentNode = _headNode;
            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = currentNode.NextNode;
            }
        }

        public TValue? Get(TKey key)
        {
            var currentNode = _headNode;

            while (currentNode != null)
            {
                if (currentNode.Key != null && currentNode.Key.Equals(key))
                {
                    var (nextNode, previousNode) = GetNeighborNodes(currentNode);

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

        private (Node<TKey, TValue>? nextNode, Node<TKey, TValue>? previousNode) GetNeighborNodes(
            Node<TKey, TValue> currentNode)
        {
            var nextNode = currentNode.NextNode;
            var previousNode = currentNode.PreviousNode;
            return (nextNode, previousNode);
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
            var newNode = new Node<TKey, TValue>(key, data);

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
            if (_currentCount == _limit)
            {
                return true;
            }

            return false;
        }

        public void Print()
        {
            DebugHelper.PrintNodes(_headNode, _tailNode);
        }
    }
}