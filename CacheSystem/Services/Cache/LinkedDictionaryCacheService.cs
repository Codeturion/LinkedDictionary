using Codeturion.Data.Structures;

namespace Codeturion.Services.Cache
{
    public class LinkedDictionaryCacheService<TKey, TValue> : ICacheService<TKey, TValue>
    {
        private readonly LinkedDictionary<TKey, TValue> _linkedDictionary;

        public LinkedDictionaryCacheService(int size)
        {
            _linkedDictionary = new LinkedDictionary<TKey, TValue>(size);
        }

        public TValue? Get(TKey key)
        {
            if (_linkedDictionary.TryGetValue(key, out var value))
            {
                return value!;
            }

            return default!;
        }

        public void Put(TKey key, TValue value)
        {
            _linkedDictionary.Add(key, value);
        }

        public void Print()
        {
            _linkedDictionary.Print();
        }
    }
}