using Codeturion.Data.Nodes;
using Codeturion.Debug;

namespace Codeturion.Data.Structures
{
    public class LinkedDictionary<TKey, TValue>
    {
        private readonly LinkedNode<TKey, TValue>[] _buckets;
        private readonly HashSet<TKey> _hashSet; // provide quick contains 

        private LinkedNode<TKey, TValue>? _headNode;
        private LinkedNode<TKey, TValue>? _tailNode;
        private bool IsFull => _hashSet.Count >= _maxSize; // discussion point

        private readonly int _maxSize;

        public LinkedDictionary(int maxSize)
        {
            _maxSize = maxSize;
            _buckets = new LinkedNode<TKey, TValue>[_maxSize];
            _hashSet = new HashSet<TKey>();
        }

        private bool Contains(TKey key)
        {
            return _hashSet.Contains(key);
        }

        public void Add(TKey key, TValue value)
        {
            if (Contains(key))
            {
                return;
            }

            if (IsFull)
            {
                RemoveTail();
            }

            var index = GetIndex(key);
            var newNode = new LinkedNode<TKey, TValue>(key, value)
            {
                NextNode = _buckets[index],
                PreviousNode = null
            };

            if (_buckets[index] != null)
            {
                _buckets[index].PreviousNode = newNode;
            }

            _buckets[index] = newNode;

            if (_headNode == null)
            {
                _headNode = _tailNode = newNode;
            }
            else
            {
                newNode.NextNode = _headNode;
                _headNode.PreviousNode = newNode;
                _headNode = newNode;
            }

            _hashSet.Add(key);
        }

        private void RemoveTail()
        {
            if (_tailNode == null)
            {
                return;
            }

            if (_tailNode.PreviousNode != null)
            {
                _tailNode.PreviousNode.NextNode = null;
            }

            if (_tailNode == _headNode)
            {
                _headNode = null;
            }

            _tailNode = _tailNode.PreviousNode;

            var index = GetIndex(_tailNode.Key);
            _hashSet.Remove(_tailNode.Key);
            _buckets[index] = null;
        }

        public bool TryGetValue(TKey key, out TValue? value)
        {
            value = default;

            if (!_hashSet.Contains(key))
            {
                return false;
            }

            var index = GetIndex(key);
            var currentNode = _buckets[index];

            if (currentNode != _headNode)
            {
                MoveNode(currentNode);
            }

            value = currentNode.Value;
            return true;
        }

        private void SetTail(LinkedNode<TKey, TValue> linkedNode)
        {
            if (linkedNode.PreviousNode != null)
            {
                _tailNode = linkedNode.PreviousNode;
            }
            else
            {
                _tailNode = _headNode;
            }
        }

        private void MoveNode(LinkedNode<TKey, TValue> linkedNode) // take a look to naming
        {
            if (linkedNode.PreviousNode != null)
            {
                linkedNode.PreviousNode.NextNode = linkedNode.NextNode;
            }

            if (linkedNode.NextNode != null)
            {
                linkedNode.NextNode.PreviousNode = linkedNode.PreviousNode;
            }

            if (linkedNode == _tailNode && linkedNode.PreviousNode != null)
            {
                _tailNode = linkedNode.PreviousNode;
            }

            linkedNode.PreviousNode = null;
            linkedNode.NextNode = _headNode;

            if (_headNode != null)
            {
                _headNode.PreviousNode = linkedNode;
            }

            _headNode = linkedNode;

            if (linkedNode == _tailNode)
            {
                SetTail(linkedNode);
            }
        }

        private int GetIndex(TKey key)
        {
            if (key == null)
            {
                return -1;
            }

            var hashCode = key.GetHashCode();
            var index = hashCode % _buckets.Length;
            return Math.Abs(index);
        }

        public void Print()
        {
            DebugHelper.PrintNodes(_headNode, _tailNode);
        }
    }
}