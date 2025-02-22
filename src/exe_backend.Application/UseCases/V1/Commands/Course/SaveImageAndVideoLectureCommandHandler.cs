using exe_backend.Contract.Services.Course;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class SaveImageLectureCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.SaveImageLectureCommand>
{
    public async Task<Result> Handle(Command.SaveImageLectureCommand command, CancellationToken cancellationToken)
    {
        var lecture = await unitOfWork.LectureRepository.FindSingleAsync(c => c.Id == command.LectureDTO.Id);

        var imageLecture = Image.Of(command.LectureDTO.ImageLecture!.PublicId!, command.LectureDTO.ImageLecture.PublicUrl!);

        lecture.Update(imageLecture: imageLecture);
        
        // Save database
        unitOfWork.LectureRepository.Update(lecture);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success
        (CourseMessage.SaveImageAndVideoLectureSuccessfully.GetMessage().Code,
        CourseMessage.SaveImageAndVideoLectureSuccessfully.GetMessage().Message));
    }
}
