using AppCleanProject.Application.Features.FVeterinarian.Commands;
using AppCleanProject.Application.Features.FVeterinarian.Dtos;
using AppCleanProject.Application.Features.FVeterinarian.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers
{
    public class VeterinariansController : ApiBaseController
    {

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var veterinarians = await Mediator
            .SendAsync<GetAllVeterinarianQuery, List<VeterinarianResponseDto>>(new GetAllVeterinarianQuery());
            return Ok(veterinarians);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegisterVeterinarianCommand request)
        {
            await Mediator.SendAsync(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateVeterinarianCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest(new { message = "Id not valid" });
            }

            var requestCopy = request with { Id = request.Id };
            await Mediator.SendAsync(requestCopy);
            return NoContent();
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult> Activate([FromBody] ActivateVeterinarianCommand request)
        {
            await Mediator.SendAsync(request);
            return NoContent();
        }
    }
}