using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Queries.Course;

public sealed class GetLectureByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetLectureByIdQuery, Success<Contract.Services.Course.Response.LectureResponse>>
{
    public async Task<Result<Success<Contract.Services.Course.Response.LectureResponse>>> Handle(Query.GetLectureByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.FindSingleAsync(u => u.Id == query.UserId, includeProperties: u => u.Subscription);

        if (user.Subscription == null)
        {
            throw new UserException.UserNotRegistPackageException();
        }

        var result = await unitOfWork.LectureRepository
            .FindSingleAsync(c => c.Id == query.LectureId, cancellationToken);

        var lectureDto = result.Adapt<LectureDTO>();

        var response = new Contract.Services.Course.Response.LectureResponse(lectureDto);

        return Result.Success(new Success<Contract.Services.Course.Response.LectureResponse>(CourseMessage.GetLectureSuccessfully.GetMessage().Code, CourseMessage.GetLectureSuccessfully.GetMessage().Message, response));
    }
}
