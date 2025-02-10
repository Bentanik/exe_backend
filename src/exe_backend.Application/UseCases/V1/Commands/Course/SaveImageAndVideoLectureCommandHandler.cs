using exe_backend.Contract.Services.Course;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class SaveImageAndVideoLectureCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.SaveImageAndVideoLectureCommand>
{
    public async Task<Result> Handle(Command.SaveImageAndVideoLectureCommand command, CancellationToken cancellationToken)
    {
        var lecture = await unitOfWork.LectureRepository.FindSingleAsync(c => c.Id == command.LectureDTO.Id);

        var imageLecture = Image.Of(command.LectureDTO.Image!.PublicId!, command.LectureDTO.Image.PublicUrl!);

        var videoLecture = Video.Of(command.LectureDTO.Video!.PublicId!, (double)command.LectureDTO.Video.Duration!);

        lecture.Update(imageLecture: imageLecture, videoLecture: videoLecture);
        
        // Save database
        unitOfWork.LectureRepository.Update(lecture);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success
        (CourseMessage.SaveImageAndVideoLectureSuccessfully.GetMessage().Code,
        CourseMessage.SaveImageAndVideoLectureSuccessfully.GetMessage().Message));
    }
}
