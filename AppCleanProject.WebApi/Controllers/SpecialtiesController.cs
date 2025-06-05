using AppCleanProject.Application.Features.FServices.Queries;
using AppCleanProject.Application.Features.FSpecialties.Command;
using AppCleanProject.Application.Features.FSpecialties.Dtos;
using AppCleanProject.Application.Features.FSpecialties.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers;

public class SpecialtiesController:ApiBaseController
{
    [HttpGet]
    public async Task<List<SpecialtiesResponseDto>> GetAll()
    {
        return await Mediator.SendAsync<GetAllSpecialtiesQuery, 
            List<SpecialtiesResponseDto>>(new GetAllSpecialtiesQuery());
    }
    
    [HttpPost]
    public async Task<ActionResult> GetById([FromBody] InsertSpecialtiesCommand request)
    {
        await Mediator.SendAsync(request);
        return Ok();
    }
}