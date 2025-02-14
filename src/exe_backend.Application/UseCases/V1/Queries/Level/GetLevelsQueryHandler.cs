using exe_backend.Contract.DTOs.LevelDTOs;
using exe_backend.Contract.Services.Level;

namespace exe_backend.Application.UseCases.V1.Queries.Level;

public sealed class GetLevelsQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetLevelsQuery, Success<Contract.Services.Level.Response.LevelsResponse>>
{
    public async Task<Result<Success<Contract.Services.Level.Response.LevelsResponse>>> Handle(Query.GetLevelsQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
       {
           return include switch
           {
               "Course" => (Expression<Func<Domain.Models.Level, object>>)(ct => ct.Courses),
               _ => throw new ArgumentException($"Unknown navigation property: {include}")
           };
       }).ToArray();

        //Find sort property without Id
        var levelsQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.LevelRepository.FindAll(includeProperties: includes) : unitOfWork.LevelRepository.FindAll(x => x.Name.Contains(query.SearchTerm), includeProperties: includes);

        // Get sort follow property
        Expression<Func<Domain.Models.Level, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            "name" => level  => level.Name,
            _ => level => level.CreatedDate!,
        };

        levelsQuery = query.SortOrder == SortOrder.Descending
             ? levelsQuery.OrderByDescending(keySelector) : levelsQuery.OrderBy(keySelector);

        var pagedResultCourse = await PagedResult<Domain.Models.Level>.CreateAsync(levelsQuery, query.PageIndex, query.PageSize);

        var levelDtos = pagedResultCourse.Items.Adapt<List<LevelDTO>>();

        var pagedResultLevelDto = PagedResult<LevelDTO>.Create(levelDtos, pagedResultCourse.PageIndex, pagedResultCourse.PageSize, pagedResultCourse.TotalCount);

        var response = new Contract.Services.Level.Response.LevelsResponse(pagedResultLevelDto);

        return Result.Success(new Success<Contract.Services.Level.Response.LevelsResponse>(LevelMessage.GetLevelSuccessfully.GetMessage().Code, LevelMessage.GetLevelSuccessfully.GetMessage().Message, response));
    }
}
