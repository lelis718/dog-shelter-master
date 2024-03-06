using DogShelterService.Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Domain
{
    public interface IDogRepository
    {
        Task Add(Dog dog, CancellationToken cancellationToken);
        Task<IEnumerable<Dog>> FindDogsBetweenAverageHeight(int minimumHeight, int maximumHeight, CancellationToken cancellationToken);
        Task<IEnumerable<Dog>> FindDogsByBreed(string breed, CancellationToken cancellationToken);
        Task<IEnumerable<Dog>> FindDogsByTemperament(string temperament, CancellationToken cancellationToken);
    }
}
