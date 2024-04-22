using System.Collections.Generic;
using Codeturion.Scripts.Data.Nodes;
using Codeturion.Scripts.Debug;
using Codeturion.Scripts.Services.Pool;
using Codeturion.Scripts.Strategies;

namespace Codeturion.Scripts.Data.Structures
{
    public class LinkedDictionary<TKey, TValue>
    {
        private readonly LinkedNode<TKey, TValue>[] _buckets;
        private readonly HashSet<TKey> _hashSet; // provide quick contains costs little memory

        private LinkedNode<TKey, TValue>? _headNode;
        private LinkedNode<TKey, TValue>? _tailNode;

        private readonly NodePoolService<TKey, TValue> _nodePoolService;
        private readonly int _poolSize = 3;

        private readonly IHashStrategy<TKey> _hashStrategy;


        private readonly int _maxSize;

        private bool IsFull => _hashSet.Count >= _maxSize;
        // Who should handle being being full? 
        // This would if it can change its size.

        public LinkedDictionary(int maxSize)
        {
            _maxSize = maxSize;
            _buckets = new LinkedNode<TKey, TValue>[_maxSize];
            _hashSet = new HashSet<TKey>();
            _nodePoolService = new NodePoolService<TKey, TValue>(_poolSize);
            _hashStrategy = new SimpleHashStrategy<TKey>();
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

            var newNode = _nodePoolService.GetNodeFromPool(key, value); //  GetNodeFromPool(key, value);
            newNode.SetNext(_buckets[index]);

            if (_buckets[index] != null)
            {
                _buckets[index].SetPrevious(newNode);
            }

            _buckets[index] = newNode;

            if (_headNode == null)
            {
                _headNode = _tailNode = newNode;
            }
            else
            {
                newNode.SetNext(_headNode);
                _headNode.SetPrevious(newNode);
                _headNode = newNode;
            }

            _hashSet.Add(key);
        }

        public bool TryGetValue(TKey key, out TValue? value)
        {
            value = default;

            if (!Contains(key))
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

        private bool Contains(TKey key)
        {
            return _hashSet.Contains(key);
        }

        private void RemoveTail()
        {
            if (_tailNode == null)
            {
                return;
            }

            _hashSet.Remove(_tailNode.Key);

            if (_tailNode == _headNode) // tail is only one
            {
                _nodePoolService.RecycleNode(_tailNode);
                _headNode = _tailNode = null;
                return;
            }

            var prevNode = _tailNode?.GetPrevious();
            prevNode?.SetNext(null);

            _nodePoolService.RecycleNode(_tailNode);

            _tailNode = prevNode;
        }

        public void Print()
        {
            DebugHelper.PrintNodes(_headNode, _tailNode);
        }

        private void SetTail(LinkedNode<TKey, TValue> linkedNode)
        {
            _tailNode = linkedNode.GetPrevious() ?? _headNode;
        }

        private void MoveNode(LinkedNode<TKey, TValue> linkedNode)
        {
            linkedNode.GetPrevious()?.SetNext(linkedNode.GetNext());
            linkedNode.GetNext()?.SetPrevious(linkedNode.GetPrevious());

            if (linkedNode == _tailNode && linkedNode.GetPrevious() != null)
            {
                _tailNode = linkedNode.GetPrevious();
            }

            linkedNode.SetPrevious(null);
            linkedNode.SetNext(_headNode);

            _headNode?.SetPrevious(linkedNode);
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

            var hashCode = _hashStrategy.GetHashCode(key);
            return _hashStrategy.GetBucketIndex(hashCode, _buckets.Length);
        }
    }
}