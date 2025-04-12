using exe_backend.Contract.DTOs.DonateDTOs;
using exe_backend.Contract.Services.Donate;

namespace exe_backend.Application.UseCases.V1.Queries.Donate;

public sealed class GetDonatesQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetDonatesQuery, Success<Contract.Services.Donate.Response.DonatesResponse>>
{
    public async Task<Result<Success<Contract.Services.Donate.Response.DonatesResponse>>> Handle(Query.GetDonatesQuery query, CancellationToken cancellationToken)
    {
        var donatesQuery = unitOfWork.DonateRepository.FindAll();


        // Get sort follow property
        Expression<Func<Domain.Models.Donate, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            _ => dish => dish.CreatedDate!,
        };

        donatesQuery = query.SortOrder == SortOrder.Descending
             ? donatesQuery.OrderByDescending(keySelector) : donatesQuery.OrderBy(keySelector);

        var pagedResult = await PagedResult<Domain.Models.Donate>.CreateAsync(donatesQuery, query.PageIndex, query.PageSize);

        var donatesDtos = pagedResult.Items.Adapt<List<DonateDto>>();

        var pagedResultDto = PagedResult<DonateDto>.Create(donatesDtos, pagedResult.PageIndex, pagedResult.PageSize, pagedResult.TotalCount, pagedResult.TotalPages);

        var response = new Contract.Services.Donate.Response.DonatesResponse(pagedResultDto);

        return Result.Success(new Success<Contract.Services.Donate.Response.DonatesResponse>("", "", response));
    }
}
