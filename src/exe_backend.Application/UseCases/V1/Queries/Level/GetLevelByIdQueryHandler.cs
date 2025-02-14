using exe_backend.Contract.DTOs.LevelDTOs;
using exe_backend.Contract.Services.Level;

namespace exe_backend.Application.UseCases.V1.Queries.Level;

public sealed class GetLevelByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetLevelByIdQuery, Success<Contract.Services.Level.Response.LevelResponse>>
{
    public async Task<Result<Success<Contract.Services.Level.Response.LevelResponse>>> Handle(Query.GetLevelByIdQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
        {
            return include switch
            {
                "Course" => (Expression<Func<Domain.Models.Level, object>>)(c => c.Courses),
                _ => throw new ArgumentException($"Unknown navigation property: {include}")
            };
        }).ToArray();

        var result = await unitOfWork.LevelRepository
            .FindSingleAsync(c => c.Id == query.LevelId, cancellationToken, includes);

        var levelDto = result.Adapt<LevelDTO>();

        var response = new Contract.Services.Level.Response.LevelResponse(levelDto);

        return Result.Success(new Success<Contract.Services.Level.Response.LevelResponse>(LevelMessage.GetLevelSuccessfully.GetMessage().Code, LevelMessage.GetLevelSuccessfully.GetMessage().Message, response));
    }
}
