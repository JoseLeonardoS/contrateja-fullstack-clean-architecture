using ContrateJa.Application.UseCases.Users.AuthenticateUser;
using ContrateJa.Application.UseCases.Users.ChangeAccountType;
using ContrateJa.Application.UseCases.Users.ChangePassword;
using ContrateJa.Application.UseCases.Users.DeactivateUser;
using ContrateJa.Application.UseCases.Users.DeleteUser;
using ContrateJa.Application.UseCases.Users.GetUserByEmail;
using ContrateJa.Application.UseCases.Users.GetUserById;
using ContrateJa.Application.UseCases.Users.ListUsers;
using ContrateJa.Application.UseCases.Users.ReactivateUser;
using ContrateJa.Application.UseCases.Users.RegisterUser;
using ContrateJa.Application.UseCases.Users.UpdateUserEmail;
using ContrateJa.Application.UseCases.Users.UpdateUserName;
using ContrateJa.Application.UseCases.Users.UpdateUserPhone;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
            => _mediator =  mediator;

        [HttpGet]
        public async Task<IActionResult> ListUsers(int page, int pageSize, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListUsersQuery(page, pageSize), ct));

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetUser(long userId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetUserByIdQuery(userId), ct));

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetUserByEmailQuery(email), ct));

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand registerUser,
            CancellationToken ct = default)
        {
            await _mediator.Send(registerUser, ct);
            return Created();
        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserCommand authenticateUser,
            CancellationToken ct = default)
            => Ok(await _mediator.Send(authenticateUser, ct));

        [HttpPut("update-name")]
        public async Task<IActionResult> UpdateUserName([FromBody] UpdateUserNameCommand updateUserName,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateUserName, ct);
            return NoContent();
        }
        
        [HttpPut("update-phone")]
        public async Task<IActionResult> UpdateUserPhone([FromBody] UpdateUserPhoneCommand updateUserPhone,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateUserPhone, ct);
            return NoContent();
        }
        
        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailCommand updateUserEmail,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateUserEmail, ct);
            return NoContent();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePassword,
            CancellationToken ct = default)
        {
            await _mediator.Send(changePassword, ct);
            return NoContent();
        }

        [HttpPut("change-account-type")]
        public async Task<IActionResult> ChangeAccountType([FromBody] ChangeAccountTypeCommand changeAccountType,
            CancellationToken ct = default)
        {
            await _mediator.Send(changeAccountType, ct);
            return NoContent();
        }

        [HttpPatch("reactivate-user")]
        public async Task<IActionResult> ReactivateUser([FromBody] ReactivateUserCommand reactivateUser,
            CancellationToken ct = default)
        {
            await _mediator.Send(reactivateUser, ct);
            return NoContent();
        }

        [HttpPatch("deactivate-user")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserCommand deactivateUser,
            CancellationToken ct = default)
        {
            await _mediator.Send(deactivateUser, ct);
            return NoContent();
        }

        [HttpDelete("{userId:long}")]
        public async Task<IActionResult> DeleteUser(long userId, CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteUserCommand(userId), ct);
            return NoContent();
        }
    }
}
