﻿using DogShelterService.Api.Domain.Entities;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace API.Test.Repositories
{
    public class TestBreed
    {
        [Fact]
        public async Task CanCalculateCorrectAverageHeight()
        {
            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Assert.Equal(12, breed.GetAverageHeight());
        }
    }
}
