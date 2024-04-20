using Codeturion.Services.Cache;

namespace Codeturion.Main;

class Program
{
    static void Main(string[] args)
    {
        ICacheService<int, string> cacheService = new LinkedDictionaryCacheService<int, string>(3);
        TestCacheService(cacheService);

        cacheService = new LinkedListCacheService<int, string>(3);
        TestCacheService(cacheService);
    }

    private static void TestCacheService(ICacheService<int, string> cacheService)
    {
        Console.WriteLine($"GET {cacheService.GetType()}");
        cacheService.Put(1, "pic1");
        cacheService.Print();
        cacheService.Put(2, "pic2");
        cacheService.Print();
        cacheService.Put(3, "pic3");
        cacheService.Print();

        Console.WriteLine($"GET {cacheService.Get(2)!}");
        cacheService.Print();

        Console.WriteLine($"GET {cacheService.Get(2)!}");
        cacheService.Print();

        Console.WriteLine($"GET {cacheService.Get(1)!}");
        cacheService.Print();

        cacheService.Put(4, "pic4");
        cacheService.Print();

        cacheService.Put(5, "pic5");
        cacheService.Print();
    }
}