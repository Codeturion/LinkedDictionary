namespace Codeturion.DataStructures
{
    public class LinkedNode<TKey,TValue> : INodeDebug
    {
        public TKey Key { get; }
        public TValue? Value { get; }
        public LinkedNode<TKey,TValue>? NextNode { get; set; }
        public LinkedNode<TKey,TValue>? PreviousNode { get; set; }

        public LinkedNode(TKey key, TValue? value)
        {
            Key = key;
            Value = value;
            NextNode = null;
            PreviousNode = null;
        }

        public (LinkedNode<TKey, TValue>? nextNode, LinkedNode<TKey, TValue>? previousNode) GetNeighborNodes()
        {
            return (NextNode, PreviousNode);
        }
        
        public string KeyAsString => Key?.ToString() ?? "";
        INodeDebug? INodeDebug.Next => NextNode;
        INodeDebug? INodeDebug.Previous => PreviousNode;
    }
}


