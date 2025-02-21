using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;
using static exe_backend.Contract.Services.Course.Event;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class CreateLectureCommandHandler
    (IUnitOfWork unitOfWork, IPublisher publisher)
    : ICommandHandler<Command.CreateLectureCommand>
{
    public async Task<Result> Handle(Command.CreateLectureCommand command, CancellationToken cancellationToken)
    {
        var chapter = await unitOfWork.ChapterRepository
            .FindSingleAsync(ct => ct.Id == command.LectureDTO.ChapterId);

        // Map to lecture
        var lecture = MapToLecture(command, chapter);

        // Save database
        unitOfWork.LectureRepository.Add(lecture);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send event
        var lectureDto = lecture.Adapt<LectureDTO>();

        var createdLectureEvent = new CreatedLectureEvent(Guid.NewGuid(), lectureDto, command.ImageFile, command.VideoFile);

        // await publisher.Publish(createdLectureEvent, cancellationToken);

        return Result.Success(new Success(CourseMessage.SaveLectureSuccessfully.GetMessage().Code, CourseMessage.SaveLectureSuccessfully.GetMessage().Message));
    }

    private static Lecture MapToLecture(Command.CreateLectureCommand createLectureCommand, Chapter chapter)
    {
        var lectureId = Guid.NewGuid();
        var lecture = Lecture.Create
        (
            id: lectureId,
            name: createLectureCommand.LectureDTO.Name!,
            description: createLectureCommand.LectureDTO.Description!
        );

        if (chapter != null)
            lecture.AssignToChapter(chapter);

        return lecture;
    }
}
