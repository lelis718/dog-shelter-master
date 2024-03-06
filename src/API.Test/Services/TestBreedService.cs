using DogShelterService.Api.Domain.Entities;
using DogShelterService.Api.Services;
using System.Linq;
using Xunit;

namespace API.Test.Services
{
    public class TestBreedService
    {
        [Fact]
        public async void CanRetrieveBreedFromThePublicApi()
        {
            BreedService service = new BreedService();
            Breed pug = (await service.FindBreedByName("pug", default)).FirstOrDefault();

            Assert.Equal(201, pug.Id);
            Assert.Equal("Pug", pug.Name);
            Assert.Equal("Lapdog", pug.BredFor);
            Assert.Equal("Toy", pug.BredGroup);
            Assert.Equal("12 - 14 years", pug.LifeSpan);
            Assert.Equal("Docile, Clever, Charming, Stubborn, Sociable, Playful, Quiet, Attentive", pug.Temperament);
            Assert.Equal("HyJvcl9N7", pug.ReferenceImageId);

        }

        [Fact]
        public async void CanHandleNotFoundBreed()
        {

            BreedService service = new BreedService();
            Breed none = (await service.FindBreedByName("somenotfoundellement", default)).FirstOrDefault();
            Assert.Null(none);

        }


        [Fact]
        public async void CanHandleInvalidStatement()
        {

            BreedService service = new BreedService();
            Breed none = (await service.FindBreedByName(";&?", default)).FirstOrDefault();
            Assert.Null(none);

        }
    }
}
