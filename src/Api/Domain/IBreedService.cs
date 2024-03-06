using DogShelterService.Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Domain
{
    public interface IBreedService
    {
        Task<IEnumerable<Breed>> FindBreedByName(string name, CancellationToken cancellationToken);
    }
}
