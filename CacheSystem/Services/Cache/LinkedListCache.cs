using Codeturion.DataStructures;

namespace Codeturion.Services.Cache
{
    public class LinkedListCache : ICache
    {
        private int _currentCount;
        private Node? _headNode;
        private Node? _tailNode;
        private readonly int _limit;
    
        public LinkedListCache(int limit)
        {
            _limit = limit;
        }
    
        public Node? Get(int key)
        {
            Node? currentNode = _headNode;

            while (currentNode != null)
            {
                if (currentNode.Key == key)
                {
                    var (nextNode, previousNode) = GetNeighborNodes(currentNode);

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
                
                    currentNode.SetNext(_headNode);
                    currentNode.SetPrevious(null);

                    _headNode?.SetPrevious(currentNode);
                    _headNode = currentNode;

                    return currentNode;
                }

                currentNode = currentNode.GetNext();
            }

            return null;
        }

        private (Node? nextNode, Node? previousNode) GetNeighborNodes(Node currentNode)
        {
            var nextNode = currentNode.GetNext();
            var previousNode = currentNode.GetPrevious();
            return (nextNode, previousNode);
        }

        public void Put(int key, string data)
        {
            if (IsFull())
            {
                var previousOfTail = _tailNode?.GetPrevious();
                previousOfTail?.SetNext(null);
            
                _tailNode = previousOfTail;
                _currentCount--;
            }
        
            Node? cachedNode = _headNode;
            Node newNode = new Node(key, data);

            if (_headNode == null)
            {
                _tailNode = newNode;
            }

            newNode.SetNext(cachedNode);

            cachedNode?.SetPrevious(newNode);

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
            Node? currentNode = _headNode;

            while (currentNode != null)
            {
                Console.Write($"({currentNode.GetPrevious()?.Key}){currentNode.Key}({currentNode.GetNext()?.Key}) ");
                currentNode = currentNode.GetNext();
            }

            Console.WriteLine($"Head:{_headNode?.Key} Tail:{_tailNode?.Key}");
        }
    }
}