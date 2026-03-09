using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Users.Shared;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.ListUsers;

public sealed class ListUsersHandler : IQueryHandler<ListUsersQuery, IReadOnlyList<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<ListUsersQuery> _validator;

    public ListUsersHandler(
        IUserRepository userRepository,
        IValidator<ListUsersQuery> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<IReadOnlyList<UserResponse>> Execute(ListUsersQuery query, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(query, ct);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var users = await _userRepository.ListAll(query.Page, query.PageSize, ct);

        return users.Select(user => new UserResponse(
                user.Id,
                user.Name.FirstName,
                user.Name.LastName,
                user.Email.Address,
                user.AccountType.ToString(),
                user.IsAvailable,
                user.State.Code,
                user.City.Name,
                user.CreatedAt))
            .ToList();
    }
}
