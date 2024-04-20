using Codeturion.DataStructures;

namespace Codeturion.Services.Cache;

public interface ICacheService<TKey,TValue>
{
    public TValue? Get(TKey key);
    public void Put(TKey key, TValue data);
    public void Print();
}