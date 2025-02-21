using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class CreateChapterCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.CreateChapterCommand>
{
    public async Task<Result> Handle(Command.CreateChapterCommand command, CancellationToken cancellationToken)
    {
        // Find course
        var course = await unitOfWork.CourseRepository.FindSingleAsync(c => c.Id == command.ChapterDTO.CourseId);

        var chapter = MapToChapter(command, course);

        unitOfWork.ChapterRepository.Add(chapter);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(
            CourseMessage.CreateChapterSuccessfully.GetMessage().Code,
            CourseMessage.CreateChapterSuccessfully.GetMessage().Message));
    }

    private static Chapter MapToChapter(Command.CreateChapterCommand createChapterCommand, Domain.Models.Course course)
    {
        var chapterId = Guid.NewGuid();

        var chapter = Chapter.Create
        (
            id: chapterId,
            name: createChapterCommand.ChapterDTO.Name!,
            description: createChapterCommand.ChapterDTO.Description!
        );

        if (course != null)
            chapter.AssignToCourse(course);

        return chapter;
    }
}
