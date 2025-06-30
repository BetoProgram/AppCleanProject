using AppCleanProject.Application.Features.FVeterinarian.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers
{
    public class VeterinariansController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegisterVeterinarianCommand request)
        {
            await Mediator.SendAsync(request);
            return Ok();
        }
    }
}