using System.Text;
using Codeturion.DataStructures;

namespace Codeturion.Services.Cache;

public class LinkedDictionaryCacheService<T> : ICacheService<T>
{
    private readonly LinkedDictionary<int, T> _linkedDictionary;

    public LinkedDictionaryCacheService(int size)
    {
        _linkedDictionary = new LinkedDictionary<int, T>(size);
    }

    public T Get(int key)
    {
        if(_linkedDictionary.TryGetValue(key,out var value))
        {
            return value!;
        }

        return default!;
    }

    public void Put(int key, T data)
    {
        _linkedDictionary.Add(key, data);
    }

    public void Print()
    {
        _linkedDictionary.Print();
    }
}