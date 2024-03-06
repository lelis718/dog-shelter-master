using DogShelterService.Api.Domain;
using DogShelterService.Api.Domain.Abstractions;
using DogShelterService.Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Features.AddDog
{
    public class SearchDogsHandler
    {
        private IDogRepository _dogRepository;
        IDictionary<string, SearchCategoryRange> _categoryRanges = new Dictionary<string, SearchCategoryRange>();

        public SearchDogsHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;

            _categoryRanges = new Dictionary<string, SearchCategoryRange>();
            _categoryRanges.Add("small", new SearchCategoryRange(0, 35));
            _categoryRanges.Add("medium", new SearchCategoryRange(36, 55));
            _categoryRanges.Add("large", new SearchCategoryRange(56, int.MaxValue));

        }
        public async Task<Result<IEnumerable<Dog>>> Handle(SearchDogsQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.Category) && string.IsNullOrEmpty(query.Breed) && string.IsNullOrEmpty(query.Temperament))
            {
                return Result.Error<IEnumerable<Dog>>(Error.BadRequestCode, "Missing Category Argument");
            }

            IEnumerable<Dog> dogs = new List<Dog>();
            if (!string.IsNullOrEmpty(query.Category))
            {
                SearchCategoryRange categoryRange = GetCategoryRange(query.Category);
                dogs = await _dogRepository.FindDogsBetweenAverageHeight(categoryRange.MinimumHeight, categoryRange.MaximumHeight, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(query.Breed))
            {
                dogs = await _dogRepository.FindDogsByBreed(query.Breed, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(query.Temperament))
            {
                dogs = await _dogRepository.FindDogsByTemperament(query.Temperament, cancellationToken);
            }
            return Result.Success(dogs);
        }

        private SearchCategoryRange GetCategoryRange(string category)
        {
            SearchCategoryRange categoryRange;
            switch (category.ToLower())
            {
                case "small":
                    categoryRange = _categoryRanges["small"];
                    break;
                case "medium":
                    categoryRange = _categoryRanges["medium"];
                    break;
                case "large":
                    categoryRange = _categoryRanges["large"];
                    break;
                default:
                    categoryRange = new SearchCategoryRange(0, 0);
                    break;
            }

            return categoryRange;
        }

        internal record SearchCategoryRange(int MinimumHeight, int MaximumHeight);


    }
}
