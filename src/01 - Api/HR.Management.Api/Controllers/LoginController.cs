using HR.Management.Application.Features.Login;
using HR.Management.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.Management.Api.Controllers;

/// <summary>
/// Controller responsible for managing login
/// </summary>
[Route("v1/api/login")]
[ApiController]
public class LoginController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    /// <summary>
    /// Do login
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}
