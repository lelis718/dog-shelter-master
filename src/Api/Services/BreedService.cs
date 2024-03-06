using DogShelterService.Api.Domain;
using DogShelterService.Api.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Services
{
    public class BreedService : IBreedService
    {
        public async Task<IEnumerable<Breed>> FindBreedByName(string name, CancellationToken cancellationToken)
        {

            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.thedogapi.com/v1/breeds/search?q={name}", cancellationToken);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Breed[] result = JsonSerializer.Deserialize<Breed[]>(json);
                return result;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
