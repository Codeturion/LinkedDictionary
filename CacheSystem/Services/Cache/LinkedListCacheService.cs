using System.Collections;
using Codeturion.DataStructures;

namespace Codeturion.Services.Cache
{
    public class LinkedListCacheService<T> : ICacheService<T>,IEnumerable
    {
        private int _currentCount;
        private Node<T>? _headNode;
        private Node<T>? _tailNode;
        private readonly int _limit;
    
        public LinkedListCacheService(int limit)
        {
            _limit = limit;
        }
    
        public IEnumerator GetEnumerator()
        {
            Node<T>? currentNode = _headNode;
            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = currentNode.GetNext();
            }
        }
        
        public T Get(int key)
        {
            Node<T>? currentNode = _headNode;

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

                    return currentNode.Data;
                }

                currentNode = currentNode.GetNext();
            }

            return default;
        }

        private (Node<T>? nextNode, Node<T>? previousNode) GetNeighborNodes(Node<T> currentNode)
        {
            var nextNode = currentNode.GetNext();
            var previousNode = currentNode.GetPrevious();
            return (nextNode, previousNode);
        }

        public void Put(int key, T data)
        {
            if (IsFull())
            {
                var previousOfTail = _tailNode?.GetPrevious();
                previousOfTail?.SetNext(null);
            
                _tailNode = previousOfTail;
                _currentCount--;
            }
        
            Node<T>? cachedNode = _headNode;
            Node<T> newNode = new Node<T>(key, data);

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
            Node<T>? currentNode = _headNode;

            while (currentNode != null)
            {
                Console.Write($"({currentNode.GetPrevious()?.Key}){currentNode.Key}({currentNode.GetNext()?.Key}) ");
                currentNode = currentNode.GetNext();
            }

            Console.WriteLine($"Head:{_headNode?.Key} Tail:{_tailNode?.Key}");
        }
    }
}