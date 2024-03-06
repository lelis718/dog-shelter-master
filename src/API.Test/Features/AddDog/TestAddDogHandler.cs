using DogShelterService.Api.Domain;
using DogShelterService.Api.Domain.Entities;
using DogShelterService.Api.Features.AddDog;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.Test.Features.AddDog
{
    public class TestAddDogHandler
    {
        [Fact]
        public async Task CanCreateADog()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();
            Mock<IBreedService> mockBreedService = new Mock<IBreedService>();

            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            mockBreedService.Setup(item => item.FindBreedByName(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<Breed>>(new[] { breed }));

            AddDogHandler handler = new AddDogHandler(mockDogRepository.Object, mockBreedService.Object);

            await handler.Handle(new AddDogCommand("someDog", "someBreed"), default);

            mockDogRepository.Verify(item => item.Add(It.IsAny<Dog>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
