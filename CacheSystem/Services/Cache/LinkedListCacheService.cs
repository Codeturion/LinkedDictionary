using Codeturion.DataStructures;

namespace Codeturion.Services.Cache
{
    public class LinkedListCacheService<TKey, TValue> : ICacheService<TKey, TValue>
    {
        private readonly SimpleLinkedList<TKey, TValue> _simpleLinkedList;

        public LinkedListCacheService(int limit)
        {
            _simpleLinkedList = new SimpleLinkedList<TKey, TValue>(limit);
        }

        public TValue? Get(TKey key)
        {
            return _simpleLinkedList.Get(key);
        }

        public void Put(TKey key, TValue value)
        {
            _simpleLinkedList.Put(key, value);
        }

        public void Print()
        {
            _simpleLinkedList.Print();
        }
    }
}