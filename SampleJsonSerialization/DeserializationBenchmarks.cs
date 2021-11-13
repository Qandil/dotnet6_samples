using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;
using System.Text;
using System.Text.Json;

namespace SampleJsonSerialization;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class DeserializationBenchmarks
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private string _peopleAsText = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
        Faker<Person> faker = new();
        Randomizer.Seed = new Random(420);
        var people = faker
            .RuleFor(x => x.FirstName, x => x.Name.FirstName())
            .RuleFor(x => x.LastName, x => x.Name.LastName())
            .Generate(1000);

        var memoryStream = new MemoryStream();
        var jsonWriter = new Utf8JsonWriter(memoryStream);
        JsonSerializer.Serialize(jsonWriter, people, _options);
        _peopleAsText = Encoding.UTF8.GetString(memoryStream.ToArray());
    }

    [Benchmark(Baseline = true)]
    public List<Person> ClassDeseializer()
    {
        return JsonSerializer.Deserialize<List<Person>>(_peopleAsText, _options)!;
    }

    [Benchmark]
    public List<Person> GeneratedDeserializer()
    {
        return (List<Person>)JsonSerializer.Deserialize(_peopleAsText, PersonJsonContext.Default.IEnumerablePerson)!;
    }
}