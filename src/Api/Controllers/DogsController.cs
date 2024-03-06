using DogShelterService.Api.Domain.Abstractions;
using DogShelterService.Api.Domain.Entities;
using DogShelterService.Api.Features.AddDog;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DogShelterService.Api.Controllers
{
    [ApiController]
    [Route("dogs")]
    public class DogsController : ControllerBase
    {

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDog([FromServices] AddDogHandler handler, string name, string breed, CancellationToken cancellationToken)
        {
            AddDogCommand command = new AddDogCommand(name, breed);
            Result result = await handler.Handle(command, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return ParseActionResult(result.ErrorMessage);
            }

        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchDogs([FromServices] SearchDogsHandler handler, [FromQuery] string category, [FromQuery] string breed, [FromQuery] string temperament, CancellationToken cancellationToken)
        {
            SearchDogsQuery query = new SearchDogsQuery(category, breed, temperament);
            Result<IEnumerable<Dog>> result = await handler.Handle(query, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return ParseActionResult(result.ErrorMessage);
            }
        }

        private IActionResult ParseActionResult(Error error)
        {
            switch (error.Code)
            {
                case Error.BadRequestCode:
                    return BadRequest(error.Message);
                case Error.NotFoundCode:
                    return NotFound(error.Message);
                default:
                    return UnprocessableEntity(error.Message);
            }
        }
    }
}
