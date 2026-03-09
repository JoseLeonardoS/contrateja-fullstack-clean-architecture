using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Users.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;

namespace ContrateJa.Application.UseCases.Users.GetUserById;

public sealed class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<UserResponse> Execute(GetUserByIdQuery query, CancellationToken ct = default)
    {
        var user = await _userRepository.GetById(query.UserId, ct);
        
        if(user is null)
            throw new NotFoundException(nameof(User),  query.UserId);
        
        return new UserResponse(
            user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Email.Address,
            user.AccountType.ToString(),
            user.IsAvailable,
            user.State.Code,
            user.City.Name,
            user.CreatedAt);
    }
}