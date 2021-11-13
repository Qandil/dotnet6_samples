using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;
using System.Text;
using System.Text.Json;

namespace SampleJsonSerialization;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class SerializationBenchmarks
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private List<Person> _people = new();

    [GlobalSetup]
    public void Setup()
    {
        Faker<Person> faker = new();
        Randomizer.Seed = new Random(420);
        _people = faker
            .RuleFor(x => x.FirstName, x => x.Name.FirstName())
            .RuleFor(x => x.LastName, x => x.Name.LastName())
            .Generate(1000);
    }

    [BenchmarkCategory("Stream"), Benchmark(Baseline = true)]
    public MemoryStream ClassSeializer()
    {
        var memoryStream = new MemoryStream();
        var jsonWriter = new Utf8JsonWriter(memoryStream);
        JsonSerializer.Serialize(jsonWriter, _people, _options);
        return memoryStream;
    }

    [BenchmarkCategory("Stream"), Benchmark]
    public MemoryStream GeneratedSerializer()
    {
        var memoryStream = new MemoryStream();
        var jsonWriter = new Utf8JsonWriter(memoryStream);
        JsonSerializer.Serialize(jsonWriter, _people, PersonJsonContext.Default.IEnumerablePerson);
        return memoryStream;
    }

    [BenchmarkCategory("String"), Benchmark(Baseline = true)]
    public string ClassSeializer_AsString()
    {
        var memoryStream = ClassSeializer();
        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }

    [BenchmarkCategory("String"), Benchmark]
    public string GeneratedSerializer_AsString()
    {
        var memoryStream = GeneratedSerializer();
        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}