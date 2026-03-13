using ContrateJa.Application.Abstractions;
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
using ContrateJa.Application.UseCases.Users.Shared;
using ContrateJa.Application.UseCases.Users.UpdateUserEmail;
using ContrateJa.Application.UseCases.Users.UpdateUserName;
using ContrateJa.Application.UseCases.Users.UpdateUserPhone;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IQueryHandler<ListUsersQuery, IReadOnlyList<UserResponse>> _listUsers;
        private readonly IQueryHandler<GetUserByIdQuery, UserResponse> _getUserById;
        private readonly IQueryHandler<GetUserByEmailQuery, UserResponse> _getUserByEmail;
        private readonly ICommandHandler<RegisterUserCommand> _registerUser;
        private readonly ICommandHandler<AuthenticateUserCommand, AuthenticateUserResponse> _authenticateUser;
        private readonly ICommandHandler<UpdateUserNameCommand> _updateUserName;
        private readonly ICommandHandler<UpdateUserPhoneCommand> _updateUserPhone;
        private readonly ICommandHandler<UpdateUserEmailCommand> _updateUserEmail;
        private readonly ICommandHandler<ChangePasswordCommand> _changePassword;
        private readonly ICommandHandler<ChangeAccountTypeCommand> _changeAccountType;
        private readonly ICommandHandler<ReactivateUserCommand> _reactivateUser;
        private readonly ICommandHandler<DeactivateUserCommand> _deactivateUser;
        private readonly ICommandHandler<DeleteUserCommand> _deleteUser;

        public UsersController(
            IQueryHandler<ListUsersQuery, IReadOnlyList<UserResponse>>  listUsers,
            IQueryHandler<GetUserByIdQuery, UserResponse> getUserById,
            IQueryHandler<GetUserByEmailQuery, UserResponse> getUserByEmail,
            ICommandHandler<RegisterUserCommand> registerUser, 
            ICommandHandler<AuthenticateUserCommand, AuthenticateUserResponse> authenticateUser, 
            ICommandHandler<UpdateUserPhoneCommand> updateUserPhone, 
            ICommandHandler<UpdateUserNameCommand> updateUserName,
            ICommandHandler<UpdateUserEmailCommand> updateUserEmail, 
            ICommandHandler<ChangePasswordCommand> changePassword, 
            ICommandHandler<ChangeAccountTypeCommand> changeAccountType, 
            ICommandHandler<ReactivateUserCommand> reactivateUser, 
            ICommandHandler<DeactivateUserCommand> deactivateUser, 
            ICommandHandler<DeleteUserCommand> deleteUser)
        {
            _listUsers = listUsers;
            _getUserById = getUserById;
            _getUserByEmail = getUserByEmail;
            _registerUser = registerUser;
            _authenticateUser = authenticateUser;
            _updateUserName = updateUserName;
            _updateUserPhone = updateUserPhone;
            _updateUserEmail = updateUserEmail;
            _changePassword = changePassword;
            _changeAccountType = changeAccountType;
            _reactivateUser = reactivateUser;
            _deactivateUser = deactivateUser;
            _deleteUser = deleteUser;
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers(int page, int pageSize, CancellationToken ct = default)
            => Ok(await  _listUsers.Execute(new ListUsersQuery(page, pageSize), ct));

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetUser(long userId, CancellationToken ct = default)
            => Ok(await _getUserById.Execute(new GetUserByIdQuery(userId), ct));

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email, CancellationToken ct = default)
            => Ok(await _getUserByEmail.Execute(new GetUserByEmailQuery(email), ct));

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand registerUser,
            CancellationToken ct = default)
        {
            await _registerUser.Execute(registerUser, ct);
            return Created();
        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserCommand authenticateUser,
            CancellationToken ct = default)
            => Ok(await _authenticateUser.Execute(authenticateUser, ct));

        [HttpPut("update-name")]
        public async Task<IActionResult> UpdateUserName([FromBody] UpdateUserNameCommand updateUserName,
            CancellationToken ct = default)
        {
            await _updateUserName.Execute(updateUserName, ct);
            return NoContent();
        }
        
        [HttpPut("update-phone")]
        public async Task<IActionResult> UpdateUserPhone([FromBody] UpdateUserPhoneCommand updateUserPhone,
            CancellationToken ct = default)
        {
            await _updateUserPhone.Execute(updateUserPhone, ct);
            return NoContent();
        }
        
        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailCommand updateUserEmail,
            CancellationToken ct = default)
        {
            await _updateUserEmail.Execute(updateUserEmail, ct);
            return NoContent();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePassword,
            CancellationToken ct = default)
        {
            await _changePassword.Execute(changePassword, ct);
            return NoContent();
        }

        [HttpPut("change-account-type")]
        public async Task<IActionResult> ChangeAccountType([FromBody] ChangeAccountTypeCommand changeAccountType,
            CancellationToken ct = default)
        {
            await _changeAccountType.Execute(changeAccountType, ct);
            return NoContent();
        }

        [HttpPatch("reactivate-user")]
        public async Task<IActionResult> ReactivateUser([FromBody] ReactivateUserCommand reactivateUser,
            CancellationToken ct = default)
        {
            await _reactivateUser.Execute(reactivateUser, ct);
            return NoContent();
        }

        [HttpPatch("deactivate-user")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserCommand deactivateUser,
            CancellationToken ct = default)
        {
            await _deactivateUser.Execute(deactivateUser, ct);
            return NoContent();
        }

        [HttpDelete("{userId:long}")]
        public async Task<IActionResult> DeleteUser(long userId, CancellationToken ct = default)
        {
            await _deleteUser.Execute(new DeleteUserCommand(userId), ct);
            return NoContent();
        }
    }
}
