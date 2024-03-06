using DogShelterService.Api.Domain;
using DogShelterService.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Repositories
{
    public class DogRepository : IDogRepository
    {
        private List<Dog> dogs;

        public DogRepository()
        {
            dogs = new List<Dog>();
        }
        public async Task Add(Dog dog, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                dogs.Add(dog);
            }, cancellationToken);
        }

        public async Task<IEnumerable<Dog>> FindDogsBetweenAverageHeight(int minimumHeight, int maximumHeight, CancellationToken cancellationToken)
        {
            return await Task.FromResult(dogs.Where(dog => dog.AverageHeight >= minimumHeight && dog.AverageHeight <= maximumHeight).ToList());
        }

        public async Task<IEnumerable<Dog>> FindDogsByBreed(string breed, CancellationToken cancellationToken)
        {
            return await Task.FromResult(dogs.Where(dog => dog.Breed.Name.Contains(breed, System.StringComparison.InvariantCultureIgnoreCase)).ToList());
        }

        public async Task<IEnumerable<Dog>> FindDogsByTemperament(string temperament, CancellationToken cancellationToken)
        {
            return await Task.FromResult(dogs
                .Where(dog =>
                dog.Breed.Temperament != null &&
                dog.Breed.Temperament.Split(",").Contains(temperament, StringComparer.InvariantCultureIgnoreCase)
                ).ToList());
        }
    }
}
