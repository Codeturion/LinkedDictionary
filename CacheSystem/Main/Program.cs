﻿using Codeturion.Services.Cache;
namespace Codeturion.Main;

class Program
{
    static void Main(string[] args)
    {
        ICache cacheService = new LinkedListCache(3);

        cacheService.Put(1, "pic1");
        cacheService.Print();
        cacheService.Put(2, "pic2");
        cacheService.Print();
        cacheService.Put(3, "pic3");
        cacheService.Print();

        Console.WriteLine($"GET {cacheService.Get(2)!.Key}");
        cacheService.Print();

        Console.WriteLine($"GET {cacheService.Get(2)!.Key}");
        cacheService.Print();


        Console.WriteLine($"GET {cacheService.Get(1)!.Key}");
        cacheService.Print();

        cacheService.Put(4, "pic4");
        cacheService.Print();

        cacheService.Put(5, "pic5");
        cacheService.Print();
    }
}