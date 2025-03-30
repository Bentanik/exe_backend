using exe_backend.Contract.Services.Course;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class SaveThumbnailCourseCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.SaveThumbnailCourseCommand>
{
    public async Task<Result> Handle(Command.SaveThumbnailCourseCommand command, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.CourseRepository.FindSingleAsync(c => c.Id == command.CourseDTO.Id);

        var thumbnail = Image.Of(command.CourseDTO.Thumbnail!.PublicId!, command.CourseDTO.Thumbnail.PublicUrl!);
        
        course.Update(thumbnail: thumbnail);
        unitOfWork.CourseRepository.Update(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success
        (CourseMessage.SaveThumbnailCourseSuccessfully.GetMessage().Code,
        CourseMessage.SaveThumbnailCourseSuccessfully.GetMessage().Message));
    }
}
