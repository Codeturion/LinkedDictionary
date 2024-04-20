using Codeturion.DataStructures;

namespace Codeturion.Services.Cache;

public interface ICache
{
    public Node? Get(int key);
    public void Put(int key, string data);
    public void Print();
}