using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;

namespace Tax.Calculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public CalculatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Calculate([Required][FromBody] CalculateTaxCommand command)
    {
        var result = await _mediator.Send(command);
        return result.WasSuccessful
            ? Ok(result.Value)
            : BadRequest(string.Join(", ", result.Errors));
    }
}
