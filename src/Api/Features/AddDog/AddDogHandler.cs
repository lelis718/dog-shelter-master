using DogShelterService.Api.Domain;
using DogShelterService.Api.Domain.Abstractions;
using DogShelterService.Api.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Features.AddDog
{
    public class AddDogHandler
    {
        private IDogRepository _dogRepository;
        private IBreedService _breedService;
        public AddDogHandler(IDogRepository dogRepository, IBreedService breedService)
        {
            _dogRepository = dogRepository;
            _breedService = breedService;
        }
        public async Task<Result> Handle(AddDogCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                return Result.Error(Error.BadRequestCode, "Missing Name");
            }
            if (string.IsNullOrEmpty(command.Breed))
            {
                return Result.Error(Error.BadRequestCode, "Missing Breed");
            }
            Breed breed = (await _breedService.FindBreedByName(command.Breed, cancellationToken)).FirstOrDefault();
            if (breed == null)
            {
                return Result.Error(Error.BadRequestCode, "Dog Breed Not Found");
            }

            Dog dog = Dog.Create(command.Name, breed);

            await _dogRepository.Add(dog, cancellationToken);

            return Result.Success();
        }

    }
}
