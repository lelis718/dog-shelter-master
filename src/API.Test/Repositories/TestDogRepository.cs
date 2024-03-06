using DogShelterService.Api.Domain.Entities;
using DogShelterService.Api.Repositories;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace API.Test.Repositories
{
    public class TestDogRepository
    {
        [Fact]
        public async Task CanAddADog()
        {
            DogRepository dogRepo = new DogRepository();
            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("Some dog", breed);
            await dogRepo.Add(dog, default);

            IEnumerable<Dog> dogs = await dogRepo.FindDogsBetweenAverageHeight(0, 12, default);
            Assert.Contains(dog, dogs);

        }
        [Fact]
        public async Task CanFilterForAverage()
        {
            DogRepository dogRepo = new DogRepository();
            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Breed breedNotInAverage = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"32 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("Some dog", breed);
            Dog dogNotInAverage = Dog.Create("Some dog", breedNotInAverage);
            await dogRepo.Add(dog, default);

            IEnumerable<Dog> dogs = await dogRepo.FindDogsBetweenAverageHeight(0, 12, default);
            Assert.Contains(dog, dogs);
            Assert.DoesNotContain(dogNotInAverage, dogs);

        }

        [Fact]
        public async Task CanFilterForBreeds()
        {
            DogRepository dogRepo = new DogRepository();
            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Breed anotherBreed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"32 - 30\"},\"id\":201,\"name\":\"AnotherKindOFDog\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("Some dog", breed);
            Dog anotherDog = Dog.Create("Some other dog", anotherBreed);
            await dogRepo.Add(dog, default);

            IEnumerable<Dog> dogs = await dogRepo.FindDogsByBreed("Pug", default);
            Assert.Contains(dog, dogs);
            Assert.DoesNotContain(anotherDog, dogs);

        }
        [Fact]
        public async Task CanFilterForTemperament()
        {
            DogRepository dogRepo = new DogRepository();
            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Breed anotherBreed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"32 - 30\"},\"id\":201,\"name\":\"AnotherKindOFDog\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("Some dog", breed);
            Dog anotherDog = Dog.Create("Some other dog", anotherBreed);
            await dogRepo.Add(dog, default);

            IEnumerable<Dog> dogs = await dogRepo.FindDogsByTemperament("Docile", default);
            Assert.Contains(dog, dogs);
            Assert.DoesNotContain(anotherDog, dogs);

        }
    }
}
