using Cortex.Mediator;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController:ControllerBase
    {
        private IMediator _mediator = null!;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
