using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Queries.Course;

public sealed class GetLecturesQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetLecturesQuery, Success<Contract.Services.Course.Response.LecturesResponse>>
{
    public async Task<Result<Success<Contract.Services.Course.Response.LecturesResponse>>> Handle(Query.GetLecturesQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
       {
           return include switch
           {
               "Chapter" => (Expression<Func<Domain.Models.Lecture, object>>)(c => c.Chapter),
               _ => throw new ArgumentException($"Unknown navigation property: {include}")
           };
       }).ToArray();

        //Find sort property without Id
        var lecturesQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.LectureRepository.FindAll(x => (query.NoneAssignedChapter == true ? x.ChapterId == null : true), includeProperties: includes) : Guid.TryParse(query.SearchTerm, out Guid chapterId)
                ? unitOfWork.LectureRepository.FindAll(
                    x => (query.NoneAssignedChapter == true ? x.ChapterId == null : x.ChapterId == chapterId), includeProperties: includes
                )
                : unitOfWork.LectureRepository.FindAll(
                    x => (x.Name.Contains(query.SearchTerm) && (query.NoneAssignedChapter == true ? x.ChapterId == null : true)), includeProperties: includes
                );
        // Get sort follow property
        Expression<Func<Domain.Models.Lecture, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            "name" => lecture => lecture.Name,
            "chapterId" => lecture => lecture.ChapterId!,
            _ => lecture => lecture.CreatedDate!,
        };

        lecturesQuery = query.SortOrder == SortOrder.Descending
             ? lecturesQuery.OrderByDescending(keySelector) : lecturesQuery.OrderBy(keySelector);

        var pagedResultLecture = await PagedResult<Domain.Models.Lecture>.CreateAsync(lecturesQuery, query.PageIndex, query.PageSize);

        var lectureDTOs = pagedResultLecture.Items.Adapt<List<LectureDTO>>();

        var pagedResultLectureDto = PagedResult<LectureDTO>.Create(lectureDTOs, pagedResultLecture.PageIndex, pagedResultLecture.PageSize, pagedResultLecture.TotalCount);

        var response = new Contract.Services.Course.Response.LecturesResponse(pagedResultLectureDto);

        return Result.Success(new Success<Contract.Services.Course.Response.LecturesResponse>(CourseMessage.GetLectureSuccessfully.GetMessage().Code, CourseMessage.GetChapterSuccessfully.GetMessage().Message, response));
    }
}
