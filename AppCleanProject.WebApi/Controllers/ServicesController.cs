using AppCleanProject.Application.Features.FServices.Commands;
using AppCleanProject.Application.Features.FServices.Dtos;
using AppCleanProject.Application.Features.FServices.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers
{
    public class ServicesController:ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ServiceResponseDto>>> GetAll()
        {
            var services = await Mediator
                .SendAsync<GetAllServicesQuery, List<ServiceResponseDto>>(new GetAllServicesQuery());
            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InsertServiceCommand request)
        {
            await Mediator.SendAsync(request);
            return Ok();    
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateServiceCommand request)
        {
            if(id != request.Id)
            {
                return BadRequest(new { message = "Id not valid" });
            }

            var requestCopy = request with { Id = request.Id };

            await Mediator.SendAsync(requestCopy);
            return NoContent();
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult> Activate([FromBody] ActivateServiceCommand request)
        {
            await Mediator.SendAsync(request);
            return NoContent();
        }
    }
}
