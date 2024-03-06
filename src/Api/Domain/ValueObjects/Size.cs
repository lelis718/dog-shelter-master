using System.Text.Json.Serialization;

namespace DogShelterService.Api.Domain.ValueObjects
{
    public record Size(
        [property: JsonPropertyName("imperial")]
        [property: JsonInclude]
        string Imperial,
        [property: JsonPropertyName("metric")]
        [property: JsonInclude]
        string Metric
        );
}
