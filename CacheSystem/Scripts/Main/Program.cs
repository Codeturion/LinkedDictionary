using BenchmarkDotNet.Running;
using Codeturion.Benchmark;
using Codeturion.Scripts.Services.Cache;

namespace Codeturion.Scripts.Main
{
    class Program
    {
        static void Main(string[] args)
        {
           // BenchmarkRunner.Run<CacheServiceBenchmark>();
            
            ICacheService<int, string> linkedDictionaryCacheService = new LinkedDictionaryCacheService<int, string>(3);
            LogCacheService(linkedDictionaryCacheService);
            
           ICacheService<int, string> linkedListCacheService = new LinkedListCacheService<int, string>(3);
           LogCacheService(linkedListCacheService);
        }

        private static void LogCacheService(ICacheService<int, string> cacheService)
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
}