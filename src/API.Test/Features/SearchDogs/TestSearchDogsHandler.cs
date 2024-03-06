using DogShelterService.Api.Domain;
using DogShelterService.Api.Features.AddDog;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.Test.Features.AddDog
{
    public class TestSearchDogsHandler
    {
        [Fact]
        public async Task CanSearchTheSmallCategory()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            var result = await handler.Handle(new SearchDogsQuery("Small", null, null), default);
            Assert.True(result.IsSuccess);

            mockDogRepository.Verify(item => item.FindDogsBetweenAverageHeight(It.Is<int>(v => v == 0), It.Is<int>(v => v == 35), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CanSearchTheMediumCategory()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            var result = await handler.Handle(new SearchDogsQuery("Medium", null, null), default);
            Assert.True(result.IsSuccess);

            mockDogRepository.Verify(item => item.FindDogsBetweenAverageHeight(It.Is<int>(v => v == 36), It.Is<int>(v => v == 55), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CanSearchTheLargeCategory()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            var result = await handler.Handle(new SearchDogsQuery("Large", null, null), default);

            Assert.True(result.IsSuccess);
            mockDogRepository.Verify(item => item.FindDogsBetweenAverageHeight(It.Is<int>(v => v == 56), It.Is<int>(v => v == int.MaxValue), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CanSendErrorOnMissingSearchFilter()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            var result = await handler.Handle(new SearchDogsQuery(null, null, null), default);
            Assert.False(result.IsSuccess);

        }


        [Fact]
        public async Task CanSearchForBreeds()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            var result = await handler.Handle(new SearchDogsQuery(null, "Some breed", null), default);

            Assert.True(result.IsSuccess);
            mockDogRepository.Verify(item => item.FindDogsByBreed(It.Is<string>(v => v == "Some breed"), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CanSearchForTemperaments()
        {
            Mock<IDogRepository> mockDogRepository = new Mock<IDogRepository>();

            SearchDogsHandler handler = new SearchDogsHandler(mockDogRepository.Object);
            var result = await handler.Handle(new SearchDogsQuery(null, null, "Some temperament"), default);

            Assert.True(result.IsSuccess);
            mockDogRepository.Verify(item => item.FindDogsByTemperament(It.Is<string>(v => v == "Some temperament"), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
