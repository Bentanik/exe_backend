using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Queries.Course;

public sealed class GetChapterByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetChapterByIdQuery, Success<Contract.Services.Course.Response.ChapterResponse>>
{
    public async Task<Result<Success<Contract.Services.Course.Response.ChapterResponse>>> Handle(Query.GetChapterByIdQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
        {
            return include switch
            {
                "Lecture" => (Expression<Func<Domain.Models.Chapter, object>>)(c => c.Lectures),
                _ => throw new ArgumentException($"Unknown navigation property: {include}")
            };
        }).ToArray();

        var result = await unitOfWork.ChapterRepository
            .FindSingleAsync(c => c.Id == query.ChapterId, cancellationToken, includes);

        var chapterDto = result.Adapt<ChapterDTO>();

        var response = new Contract.Services.Course.Response.ChapterResponse(chapterDto);

        return Result.Success(new Success<Contract.Services.Course.Response.ChapterResponse>(CourseMessage.GetChapterSuccessfully.GetMessage().Code, CourseMessage.GetChapterSuccessfully.GetMessage().Message, response));
    }
}
