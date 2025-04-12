using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Queries.User;

public sealed class GetUsersQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetUsersQuery, Success<Contract.Services.User.Response.UsersResponse>>
{
    public async Task<Result<Success<Contract.Services.User.Response.UsersResponse>>> Handle(Query.GetUsersQuery query, CancellationToken cancellationToken)
    {
        var usersQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.UserRepository.FindAll()
            : unitOfWork.UserRepository.FindAll
            (x => x.FullName.Contains(query.SearchTerm) || x.Email.Contains(query.SearchTerm));


        // Get sort follow property
        Expression<Func<Domain.Models.User, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            _ => dish => dish.CreatedDate!,
        };

        usersQuery = query.SortOrder == SortOrder.Descending
             ? usersQuery.OrderByDescending(keySelector) : usersQuery.OrderBy(keySelector);

        var pagedResult = await PagedResult<Domain.Models.User>.CreateAsync(usersQuery, query.PageIndex, query.PageSize);

        var usersDtos = pagedResult.Items.Adapt<List<UserDto>>();

        var pagedResultDto = PagedResult<UserDto>.Create(usersDtos, pagedResult.PageIndex, pagedResult.PageSize, pagedResult.TotalCount, pagedResult.TotalPages);

        var response = new Contract.Services.User.Response.UsersResponse(pagedResultDto);

        return Result.Success(new Success<Contract.Services.User.Response.UsersResponse>("", "", response));
    }
}