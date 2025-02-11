using exe_backend.Contract.Common.Enums;
using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Queries.Course;

public sealed class GetCoursesQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetCoursesQuery, Success<Contract.Services.Course.Response.CoursesResponse>>
{
    public async Task<Result<Success<Contract.Services.Course.Response.CoursesResponse>>> Handle(Query.GetCoursesQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
        {
            return include switch
            {
                "Chapter" => (Expression<Func<Domain.Models.Course, object>>)(c => c.Chapters),
                _ => throw new ArgumentException($"Unknown navigation property: {include}")
            };
        }).ToArray();

        //Find sort property without Id
        var coursesQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.CourseRepository.FindAll(includeProperties: includes) : unitOfWork.CourseRepository.FindAll(x => x.Name.Contains(query.SearchTerm), includeProperties: includes);

        // Get sort follow property
        Expression<Func<Domain.Models.Course, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            "name" => course => course.Name,
            _ => course => course.CreatedDate!,
        };

        coursesQuery = query.SortOrder == SortOrder.Descending
             ? coursesQuery.OrderByDescending(keySelector) : coursesQuery.OrderBy(keySelector);

        var pagedResultCourse = await PagedResult<Domain.Models.Course>.CreateAsync(coursesQuery, query.PageIndex, query.PageSize);

        var courseDtos = pagedResultCourse.Items.Adapt<List<CourseDTO>>();

        var pagedResultCourseDto = PagedResult<CourseDTO>.Create(courseDtos, pagedResultCourse.PageIndex, pagedResultCourse.PageSize, pagedResultCourse.TotalCount);

        var response = new Contract.Services.Course.Response.CoursesResponse(pagedResultCourseDto);

        return Result.Success(new Success<Contract.Services.Course.Response.CoursesResponse>(CourseMessage.GetCourseSuccessfully.GetMessage().Code, CourseMessage.GetCourseSuccessfully.GetMessage().Message, response));
    }
}
