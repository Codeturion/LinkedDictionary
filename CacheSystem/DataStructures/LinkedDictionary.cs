namespace Codeturion.DataStructures;

public class LinkedDictionary<TKey, TValue>
{
    public class KeyValueNode
    {
        public TKey Key { get; set; }
        public TValue? Value { get; set; }
        public KeyValueNode? NextNode { get; set; }
        public KeyValueNode? PreviousNode { get; set; }

        public KeyValueNode(TKey key, TValue? value)
        {
            Key = key;
            Value = value;
            NextNode = null;
            PreviousNode = null;
        }

        public KeyValueNode(TKey key, TValue? value, KeyValueNode nextNode, KeyValueNode previousNode)
        {
            Key = key;
            Value = value;
            NextNode = nextNode;
            PreviousNode = previousNode;
        }
    }

    private readonly KeyValueNode[] _buckets;
    private readonly HashSet<TKey> _hashSet; // provide quick contains
    private KeyValueNode? headNode;
    private KeyValueNode? tailNode;
    private bool IsFull => _hashSet.Count >= _maxSize;

    private int _maxSize;

    public LinkedDictionary(int maxSize)
    {
        _maxSize = maxSize;
        _buckets = new KeyValueNode[_maxSize];
        _hashSet = new HashSet<TKey>();
    }

    public bool Contains(TKey key)
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

        int index = GetIndex(key);
        KeyValueNode newNode = new KeyValueNode(key, value)
        {
            NextNode = _buckets[index],
            PreviousNode = null
        };

        if (_buckets[index] != null)
        {
            _buckets[index].PreviousNode = newNode;
        }

        _buckets[index] = newNode;

        if (headNode == null)
        {
            headNode = tailNode = newNode;
        }
        else
        {
            newNode.NextNode = headNode;
            headNode.PreviousNode = newNode;
            headNode = newNode;
        }

        _hashSet.Add(key);
    }

    private void RemoveTail()
    {
        if (tailNode == null)
        {
            return;
        }

        if (tailNode.PreviousNode != null)
        {
            tailNode.PreviousNode.NextNode = null;
        }

        if (tailNode == headNode)
        {
            headNode = null;
        }

        tailNode = tailNode.PreviousNode;

        int index = GetIndex(tailNode.Key);
        _hashSet.Remove(tailNode.Key);
        _buckets[index] = null;
    }

    public bool TryGetValue(TKey key, out TValue? value)
    {
        value = default;

        if (!_hashSet.Contains(key))
        {
            return false;
        }

        int index = GetIndex(key);
        KeyValueNode currentNode = _buckets[index];

        if (currentNode != headNode)
        {
            MoveNode(currentNode);
        }

        value = currentNode.Value;
        return true;
    }

    private void SetTail(KeyValueNode node)
    {
        if (node.PreviousNode != null)
        {
            tailNode = node.PreviousNode;
        }
        else
        {
            tailNode = headNode;
        }
    }

    private void MoveNode(KeyValueNode node) // take a look to naming
    {
        if (node.PreviousNode != null)
        {
            node.PreviousNode.NextNode = node.NextNode;
        }

        if (node.NextNode != null)
        {
            node.NextNode.PreviousNode = node.PreviousNode;
        }

        if (node == tailNode && node.PreviousNode != null)
        {
            tailNode = node.PreviousNode;
        }

        node.PreviousNode = null;
        node.NextNode = headNode;

        if (headNode != null)
        {
            headNode.PreviousNode = node;
        }

        headNode = node;

        if (node == tailNode)
        {
            SetTail(node);
        }
    }

    private int GetIndex(TKey key)
    {
        if (key == null)
        {
            return -1;
        }

        int hashCode = key.GetHashCode();
        int index = hashCode % _buckets.Length;
        return Math.Abs(index);
    }
    
    public void Print()
    {
        KeyValueNode? currentNode = headNode;
        while (currentNode != null)
        {
            string prevKey = currentNode.PreviousNode?.Key?.ToString() ?? "";
            string nextKey = currentNode.NextNode?.Key?.ToString() ?? "";
            Console.Write($"({prevKey}){currentNode.Key}({nextKey}) ");
            currentNode = currentNode.NextNode;
        }

        if (headNode != null && tailNode != null)
        {
            Console.WriteLine($"Head:{headNode.Key} Tail:{tailNode.Key}");
        }
    }
}