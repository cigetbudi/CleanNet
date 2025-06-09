using CleanNet.Application.CatFacts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanNet.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CatFactController : Controller
{
    private readonly IMediator _mediator;

    public CatFactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("fetch-and-save")]
    public async Task<IActionResult> FetchAndSave()
    {
        await _mediator.Send(new GetAndSaveCatFactCommand());
        return Ok("Saved");
    }

}
