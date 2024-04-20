using Codeturion.DataStructures;

namespace Codeturion.Services.Cache;

public interface ICacheService<T>
{
    public T Get(int key);
    public void Put(int key, T data);
    public void Print();
}