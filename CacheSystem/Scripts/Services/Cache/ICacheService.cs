namespace Codeturion.Scripts.Services.Cache
{
    public interface ICacheService<TKey,TValue>
    {
        public TValue? Get(TKey key);
        public void Put(TKey key, TValue value);
        public void Print();
    }
}