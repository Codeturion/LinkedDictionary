using System.Collections.Generic;
using Codeturion.Scripts.Data.Nodes;

namespace Codeturion.Scripts.Services.Pool;

public class NodePoolService<TKey, TValue>
{
    private readonly Stack<LinkedNode<TKey, TValue>> _nodePool; // lets try the pool to reuse deleted ones.

    public NodePoolService(int poolSize)
    {
        _nodePool = new Stack<LinkedNode<TKey, TValue>>(poolSize);
    }
    
    public LinkedNode<TKey, TValue> GetNodeFromPool(TKey key, TValue value)
    {
        LinkedNode<TKey, TValue> node;
        if (_nodePool.Count > 0)
        {
            node = _nodePool.Pop();
            node.SetKeyValue(key, value);
        }
        else
        {
            node = new LinkedNode<TKey, TValue>(key, value);
        }

        return node;
    }

    public void RecycleNode(LinkedNode<TKey, TValue> node)
    {
        node.Reset();
        _nodePool.Push(node);
    }
}