using DogShelterService.Api.Domain.ValueObjects;
using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DogShelterService.Api.Domain.Entities
{
    public class Breed
    {
        [JsonInclude]
        [JsonPropertyName("height")]
        public Size Height { get; private set; }

        [JsonInclude]
        [JsonPropertyName("weight")]
        public Size Weight { get; private set; }
        [JsonInclude]
        [JsonPropertyName("id")]
        public int Id { get; private set; }
        [JsonInclude]
        [JsonPropertyName("name")]
        public string Name { get; private set; }
        [JsonInclude]
        [JsonPropertyName("bred_for")]
        public string BredFor { get; private set; }

        [JsonInclude]
        [JsonPropertyName("breed_group")]
        public string BredGroup { get; private set; }
        [JsonInclude]
        [JsonPropertyName("life_span")]
        public string LifeSpan { get; private set; }
        [JsonInclude]
        [JsonPropertyName("temperament")]

        public string Temperament { get; private set; }
        [JsonInclude]
        [JsonPropertyName("reference_image_id")]
        public string ReferenceImageId { get; private set; }

        public int GetAverageHeight()
        {
            Regex pattern = new Regex("(\\d+) - (\\d)+");
            var heightGroups = pattern.Match(Height.Metric).Groups;
            int heightA = Int32.Parse(heightGroups[1].Value);
            int heightB = Int32.Parse(heightGroups[2].Value);

            return (heightA + heightB) / 2;

        }
    }
}
