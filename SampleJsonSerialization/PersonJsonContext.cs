using System.Text.Json.Serialization;

namespace SampleJsonSerialization;

[JsonSerializable(typeof(IEnumerablePerson))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class PersonJsonContext : JsonSerializerContext
{
}
