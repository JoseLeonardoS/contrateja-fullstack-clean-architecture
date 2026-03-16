using ContrateJa.Application.UseCases.Users.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.ListUsers;

public sealed class ListUsersQuery(int page, int pageSize) : IRequest<IReadOnlyList<UserResponse>>
{
    public int Page { get; } = page > 0
        ? page
        : throw new ArgumentOutOfRangeException(nameof(page));

    public int PageSize { get; } = pageSize > 0
        ? pageSize
        : throw new ArgumentOutOfRangeException(nameof(pageSize));
}