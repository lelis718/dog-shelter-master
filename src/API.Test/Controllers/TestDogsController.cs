using DogShelterService.Api.Controllers;
using DogShelterService.Api.Domain;
using DogShelterService.Api.Domain.Entities;
using DogShelterService.Api.Features.AddDog;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.Test.Controllers
{
    public class TestDogsController
    {


        [Fact]
        public async Task CreateDogCanFlowThroughTheHappyPath()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();
            Mock<IBreedService> mockBreedService = new Mock<IBreedService>();

            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            mockBreedService.Setup(item => item.FindBreedByName(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<Breed>>(new[] { breed }));

            AddDogHandler handler = new AddDogHandler(mockDogRepository.Object, mockBreedService.Object);
            DogsController controller = new DogsController();
            var result = await controller.CreateDog(handler, "Some dog", "Some breed", default);

            var ok = result as OkResult;

            Assert.NotNull(ok);

        }


        [Fact]
        public async Task CreateDogCanSendErrorsFromHandler()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();
            Mock<IBreedService> mockBreedService = new Mock<IBreedService>();

            mockBreedService.Setup(item => item.FindBreedByName(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<Breed>>(new List<Breed>()));

            AddDogHandler handler = new AddDogHandler(mockDogRepository.Object, mockBreedService.Object);
            DogsController controller = new DogsController();
            var result = await controller.CreateDog(handler, null, "Some breed", default);

            var bad = result as BadRequestObjectResult;

            Assert.NotNull(bad);

        }


        [Fact]
        public async Task SearchDogCanSearchForCategories()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("some dog", breed);
            mockDogRepository.Setup(i => i.FindDogsBetweenAverageHeight(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<Dog>>(new[] { dog }));

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            DogsController controller = new DogsController();
            var result = await controller.SearchDogs(handler, "Small", null, null, default);

            var ok = result as OkObjectResult;

            Assert.NotNull(ok);

        }

        [Fact]
        public async Task SearchDogCanHandleInvalidData()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("some dog", breed);
            mockDogRepository.Setup(i => i.FindDogsBetweenAverageHeight(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<Dog>>(new[] { dog }));

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            DogsController controller = new DogsController();
            var result = await controller.SearchDogs(handler, "Not on Category", null, null, default);

            var ok = result as OkObjectResult;

            Assert.NotNull(ok);

        }

        [Fact]
        public async Task SearchDogDoNotSearchOnEnptyInput()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            Breed breed = JsonSerializer.Deserialize<Breed>("{\"weight\":{\"imperial\":\"14 - 18\",\"metric\":\"6 - 8\"},\"height\":{\"imperial\":\"10 - 12\",\"metric\":\"25 - 30\"},\"id\":201,\"name\":\"Pug\",\"bred_for\":\"Lapdog\",\"breed_group\":\"Toy\",\"life_span\":\"12 - 14 years\",\"temperament\":\"Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive\",\"reference_image_id\":\"HyJvcl9N7\"}");
            Dog dog = Dog.Create("some dog", breed);
            mockDogRepository.Setup(i => i.FindDogsBetweenAverageHeight(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<Dog>>(new[] { dog }));

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            DogsController controller = new DogsController();
            var result = await controller.SearchDogs(handler, null, null, null, default);

            var bad = result as BadRequestObjectResult;

            Assert.NotNull(bad);

        }
    }
}
