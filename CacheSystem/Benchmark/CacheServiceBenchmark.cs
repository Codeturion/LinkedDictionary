using BenchmarkDotNet.Attributes;
using Codeturion.Services.Cache;

namespace Codeturion.Benchmark;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class CacheServiceBenchmark
{
    private ICacheService<int, string> dictionaryCacheService;
    private ICacheService<int, string> linkedListCacheService;

    [Params(25, 250, 25000)] public int N { get; set; }

    [IterationSetup(Targets = new[] { $"{nameof(LinkedDictionaryGetBenchmark)}", $"{nameof(LinkedListGetBenchmark)}"})]
    public void SetupGet()
    {
        dictionaryCacheService = new LinkedDictionaryCacheService<int, string>(250);
        linkedListCacheService = new LinkedListCacheService<int, string>(250);
        
        for (int i = 0; i < N; i++)
        {
            dictionaryCacheService.Put(i, $"Value{i}");
            linkedListCacheService.Put(i, $"Value{i}");
        }
    }

    [IterationSetup(Targets = new[] { $"{nameof(LinkedDictionaryPutBenchmark)}", $"{nameof(LinkedListPutBenchmark)}"})]
    public void SetupPut()
    {
        dictionaryCacheService = new LinkedDictionaryCacheService<int, string>(250);
        linkedListCacheService = new LinkedListCacheService<int, string>(250);
    }

    [Benchmark]
    public void LinkedDictionaryGetBenchmark()
    {
        for (int i = 0; i < N; i++)
        {
            dictionaryCacheService.Get(i);
        }
    }

    [Benchmark]
    public void LinkedListGetBenchmark()
    {
        for (int i = 0; i < N; i++)
        {
            linkedListCacheService.Get(i);
        }
    }

    [Benchmark]
    public void LinkedDictionaryPutBenchmark()
    {
        for (int i = 0; i < N; i++)
        {
            dictionaryCacheService.Put(i, $"Value{i}");
        }
    }

    [Benchmark]
    public void LinkedListPutBenchmark()
    {
        for (int i = 0; i < N; i++)
        {
            linkedListCacheService.Put(i, $"Value{i}");
        }
    }
}