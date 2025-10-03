using HR.Management.Application.Features.Users.Create;
using HR.Management.Application.Features.Users.Get;
using HR.Management.Application.Features.Users.GetById;
using HR.Management.Domain.Helper;
using HR.Management.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.Management.Api.Controllers;

/// <summary>
/// Controller responsible for managing users
/// </summary>
/// <param name="mediator"></param>
[Route("v1/api/users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Register an user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = RolesHelper.ADMIN)]
    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsers(
    CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();

        var users = await _mediator.Send(query, cancellationToken);

        if (users is null)
        {
            return NotFound();
        }

        return Ok(users);
    }

    /// <summary>
    /// Get user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        var user = await _mediator.Send(query, cancellationToken);

        return user is null 
            ? NotFound() 
            : Ok(user);
    }

}
