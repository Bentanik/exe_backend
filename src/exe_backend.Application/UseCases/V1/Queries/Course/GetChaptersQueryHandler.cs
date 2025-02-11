using exe_backend.Contract.Common.Enums;
using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Queries.Course;

public sealed class GetChaptersQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetChaptersQuery, Success<Contract.Services.Course.Response.ChaptersResponse>>
{
    public async Task<Result<Success<Contract.Services.Course.Response.ChaptersResponse>>> Handle(Query.GetChaptersQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
                {
                    return include switch
                    {
                        "Lecture" => (Expression<Func<Domain.Models.Chapter, object>>)(c => c.Lectures),
                        _ => throw new ArgumentException($"Unknown navigation property: {include}")
                    };
                }).ToArray();


        //Find sort property without Id
        var chaptersQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.ChapterRepository.FindAll(includeProperties: includes) : Guid.TryParse(query.SearchTerm, out Guid courseId)
                ? unitOfWork.ChapterRepository.FindAll(
                    x => x.CourseId == courseId,
                    includeProperties: includes
                )
                : unitOfWork.ChapterRepository.FindAll(
                    x => x.Name.Contains(query.SearchTerm),
                    includeProperties: includes
                );
        // Get sort follow property
        Expression<Func<Domain.Models.Chapter, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            "name" => chapter => chapter.Name,
            "courseId" => chapter => chapter.CourseId!,
            _ => chapter => chapter.CreatedDate!,
        };

        chaptersQuery = query.SortOrder == SortOrder.Descending
             ? chaptersQuery.OrderByDescending(keySelector) : chaptersQuery.OrderBy(keySelector);

        var pagedResultCourse = await PagedResult<Domain.Models.Chapter>.CreateAsync(chaptersQuery, query.PageIndex, query.PageSize);

        var chapterDTOs = pagedResultCourse.Items.Adapt<List<ChapterDTO>>();

        var pagedResultChapterDto = PagedResult<ChapterDTO>.Create(chapterDTOs, pagedResultCourse.PageIndex, pagedResultCourse.PageSize, pagedResultCourse.TotalCount);

        var response = new Contract.Services.Course.Response.ChaptersResponse(pagedResultChapterDto);


        return Result.Success(new Success<Contract.Services.Course.Response.ChaptersResponse>(CourseMessage.GetChapterSuccessfully.GetMessage().Code, CourseMessage.GetChapterSuccessfully.GetMessage().Message, response));
    }
}
