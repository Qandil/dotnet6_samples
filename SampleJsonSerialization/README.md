# Sample Json Serialization with Benchmarks
 
Based on Nick's video [40% faster JSON serialization with Source Generators in .NET 6](https://www.youtube.com/watch?v=HhyBaJ7uisU)


``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
``` 

**Seializer**
|                       Method | Categories |     Mean |   Error |  StdDev | Ratio | RatioSD |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|----------------------------- |----------- |---------:|--------:|--------:|------:|--------:|--------:|--------:|--------:|----------:|
|               ClassSeializer |     Stream | 232.9 μs | 4.54 μs | 9.06 μs |  1.00 |    0.00 | 37.3535 | 12.2070 |       - |    155 KB |
|          GeneratedSerializer |     Stream | 131.5 μs | 1.56 μs | 1.30 μs |  0.56 |    0.02 | 37.3535 | 12.2070 |       - |    154 KB |
|                              |            |          |         |         |       |         |         |         |         |           |
|      ClassSeializer_AsString |     String | 355.3 μs | 3.82 μs | 3.75 μs |  1.00 |    0.00 | 53.7109 | 26.8555 | 26.8555 |    282 KB |
| GeneratedSerializer_AsString |     String | 258.2 μs | 3.00 μs | 2.81 μs |  0.73 |    0.01 | 53.7109 | 26.8555 | 26.8555 |    282 KB |


**Deseializer**
|                Method |     Mean |   Error |   StdDev |   Median | Ratio | RatioSD |   Gen 0 |  Gen 1 | Allocated |
|---------------------- |---------:|--------:|---------:|---------:|------:|--------:|--------:|-------:|----------:|
|      ClassDeseializer | 418.7 μs | 8.31 μs | 12.19 μs | 413.0 μs |  1.00 |    0.00 | 27.3438 | 8.7891 |    121 KB |
| GeneratedDeserializer | 434.8 μs | 8.45 μs | 19.24 μs | 426.8 μs |  1.06 |    0.05 | 27.8320 | 9.2773 |    121 KB |