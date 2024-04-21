namespace Codeturion.Data.Nodes
{
    public class LinkedNode<TKey, TValue> : INodeDebug
    {
        public TKey Key { get; }
        public TValue? Value { get; }
        private LinkedNode<TKey, TValue>? NextNode { get; set; }
        private LinkedNode<TKey, TValue>? PreviousNode { get; set; }

        public LinkedNode(TKey key, TValue? value, LinkedNode<TKey, TValue>? previousNode = null,
            LinkedNode<TKey, TValue>? nextNode = null)
        {
            Key = key;
            Value = value;
            NextNode = nextNode;
            PreviousNode = previousNode;
        }

        #region Node Helpers

        public (LinkedNode<TKey, TValue>? nextNode, LinkedNode<TKey, TValue>? previousNode) GetNeighborNodes()
        {
            return (NextNode, PreviousNode);
        }

        #endregion Node Helpers

        #region Setters

        public void SetNext(LinkedNode<TKey, TValue>? nextNode)
        {
            NextNode = nextNode;
        }

        public void SetPrevious(LinkedNode<TKey, TValue>? previousNode)
        {
            PreviousNode = previousNode;
        }

        #endregion Setters

        #region Getters

        public LinkedNode<TKey, TValue>? GetPrevious()
        {
            return PreviousNode;
        }

        public LinkedNode<TKey, TValue>? GetNext()
        {
            return NextNode;
        }

        #endregion Getters

        #region Debug

        public string KeyAsString => Key?.ToString() ?? "";
        INodeDebug? INodeDebug.Next => NextNode;
        INodeDebug? INodeDebug.Previous => PreviousNode;

        #endregion Debug
    }
}