# Performance Benchmark Results

## Overview

This document outlines benchmark results comparing the performance and scalability of Doubly Linked Lists (DLL) and Linked Dictionaries for 'Get' operations in our project.

### Environments

- **Operating System**: [Windows 10]
- **CPU**: [Specify CPU, e.g., Intel i7-13700K]
- **Memory**: [32GB DDR5]
- **Tool Used**: [BenchmarkDotNet]

### Data Structures Tested

- **Doubly Linked List**: Implements a traditional double-ended linked list, allowing sequential access to elements.
- **LinkedDictionary**: Custom implementation combining hash table indexing with linked list elements to optimize access times.

## Benchmark Methodology

Benchmark tests were carried out to measure the duration taken for the 'Get' operations across datasets of different sizes. Whenever the cache gets full, it removes the last element. Each structure was filled with integer keys that were linked with simple string values. The performance of the structures was recorded under three different scenarios.
1. **Small Scale**:  The capacity of the cache is 250 elements
2. **Medium Scale**: The capacity of the cache is 2,500 elements
3. **Large Scale**: The capacity of the cache is 25,000 elements.
4. **Node Recycler**: When a node is no longer needed, instead of being destroyed, it undergoes the recycling process.

Each test was repeated five times for consistency, and the average time was noted.

## Results

### Summary

The results clearly indicate that **LinkedDictionary** scales more efficiently compared to the **Doubly Linked List**, particularly as the number of elements increases. The performance data is as follows:

- **Doubly Linked List** vs **LinkedDictionary**
  - Small Scale: ![Optional Alt Text](https://i.imgur.com/gCnAUo9.png)
  - Medium Scale: ![Optional Alt Text](https://i.imgur.com/f86j2N6.png)
  - Large Scale: ![Optional Alt Text](https://i.imgur.com/CzswmVR.png)
    
  - Node Recycler ON: ![Optional Alt Text](https://i.imgur.com/wau3rLF.png)
  - Node Recycler OFF: ![Optional Alt Text](https://i.imgur.com/Dk01gCX.png)

## Conclusion

The benchmarks demonstrate that the LinkedDictionary offers superior scalability and efficiency in 'Get' operations compared to a traditional Doubly Linked List, especially noticeable at larger scales..

## Future Work

Further optimizations may involve:
- Enhancing node recycling in LinkedDictionary to reduce memory allocations further.
- Exploring additional data structures for comparative analysis.

## Additional Notes

- Node recycling is currently implemented only in the LinkedDictionary and significantly reduces memory overhead at high scales.
