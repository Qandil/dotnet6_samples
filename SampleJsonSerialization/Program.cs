using BenchmarkDotNet.Running;
using SampleJsonSerialization;

BenchmarkRunner.Run<SerializationBenchmarks>();
BenchmarkRunner.Run<DeserializationBenchmarks>();