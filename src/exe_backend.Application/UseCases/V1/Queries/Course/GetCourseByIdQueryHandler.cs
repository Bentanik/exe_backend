using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Queries.Course;

public sealed class GetCourseByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetCourseByIdQuery, Success<Contract.Services.Course.Response.CourseResponse>>
{
    public async Task<Result<Success<Contract.Services.Course.Response.CourseResponse>>> Handle(Query.GetCourseByIdQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
       {
           return include switch
           {
               "Chapter" => (Expression<Func<Domain.Models.Course, object>>)(c => c.Chapters),
               _ => throw new ArgumentException($"Unknown navigation property: {include}")
           };
       }).ToArray();

        var result = await unitOfWork.CourseRepository
            .FindSingleAsync(c => c.Id == query.CourseId, cancellationToken, includes);

        var courseDto = result.Adapt<CourseDTO>();

        var response = new Contract.Services.Course.Response.CourseResponse(courseDto);

        return Result.Success(new Success<Contract.Services.Course.Response.CourseResponse>(CourseMessage.GetCourseSuccessfully.GetMessage().Code, CourseMessage.GetCourseSuccessfully.GetMessage().Message, response));
    }
}
