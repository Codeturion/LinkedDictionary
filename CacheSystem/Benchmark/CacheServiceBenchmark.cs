using BenchmarkDotNet.Attributes;
using Codeturion.Scripts.Services.Cache;

namespace Codeturion.Benchmark;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class CacheServiceBenchmark
{
    private ICacheService<int, string> dictionaryCacheService;
    private ICacheService<int, string> linkedListCacheService;

    private int _cacheSize = 250;
    [Params(250, 2500, 25000)] public int N { get; set; }

    [IterationSetup(Targets = new[] { $"{nameof(LinkedDictionaryGetBenchmark)}", $"{nameof(LinkedListGetBenchmark)}" })]
    public void SetupGet()
    {
        dictionaryCacheService = new LinkedDictionaryCacheService<int, string>(_cacheSize);
        linkedListCacheService = new LinkedListCacheService<int, string>(_cacheSize);

        for (int i = 0; i < N; i++)
        {
            dictionaryCacheService.Put(i, $"Value{i}");
            linkedListCacheService.Put(i, $"Value{i}");
        }
    }

    [IterationSetup(Targets = new[] { $"{nameof(LinkedDictionaryPutBenchmark)}", $"{nameof(LinkedListPutBenchmark)}" })]
    public void SetupPut()
    {
        dictionaryCacheService = new LinkedDictionaryCacheService<int, string>(_cacheSize);
        linkedListCacheService = new LinkedListCacheService<int, string>(_cacheSize);
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