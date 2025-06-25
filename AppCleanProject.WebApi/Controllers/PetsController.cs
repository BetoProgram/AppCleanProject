using AppCleanProject.Application.Features.Pet.Commands;
using AppCleanProject.Application.Features.Pet.Dtos;
using AppCleanProject.Application.Features.Pet.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers
{
    [Authorize]
    public class PetsController: ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var pets = await Mediator
                .SendAsync<GetPetsCustomerQuery, List<PetsResponseDto>>(new GetPetsCustomerQuery());

            return Ok(pets);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOwnerPetCommand request)
        {
            await Mediator.SendAsync(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] UpdatePetCommand request)
        {
            if(id != request.Id)
            {
                return BadRequest(new { message = "Id not valid from pet" });
            }
            var requestCopy = request with { Id = request.Id };

            await Mediator.SendAsync(requestCopy);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(long id)
        {
            await Mediator.SendAsync(new RemovePetCommand { Id = id });
            return NoContent();
        }
    }
}
