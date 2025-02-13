using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;
using static exe_backend.Contract.Services.Course.Event;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class CreateCourseCommandHandler
    (IUnitOfWork unitOfWork, IPublisher publisher)
    : ICommandHandler<Command.CreateCourseCommand>
{
    public async Task<Result> Handle(Command.CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var isCheckCourseName = await unitOfWork.CourseRepository
            .AnyAsync(c => c.Name == command.Name);

        // If courseName find
        if (isCheckCourseName == true)
            throw new CourseException.CourseNameDuplicateException();

        // Create course
        var course = MapToCourse(command);
        // Validation with case create course have category andc category must type Guid
        if (command.CategoryId != null && command.CategoryId != Guid.Empty)
        {
            var category = await unitOfWork.CategoryRepository
                .FindSingleAsync(ct => ct.Id == command.CategoryId);

            // Found category, the course will be assigned to that
            if (category != null)
            {
                course.AssignCategory(category);
            }            
        }

        // Save database
        unitOfWork.CourseRepository.Add(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send event
        var courseDto = course.Adapt<CourseDTO>();
        var createdCourseEvent = new CreatedCourseEvent(Guid.NewGuid(), courseDto, command.ThumbnailFile);

        await publisher.Publish(createdCourseEvent, cancellationToken);

        return Result.Success(new Success(CourseMessage.SaveCourseSuccessfully.GetMessage().Code, CourseMessage.SaveCourseSuccessfully.GetMessage().Message));
    }

    private static Domain.Models.Course MapToCourse(Command.CreateCourseCommand createCourseCommand)
    {
        var courseId = Guid.NewGuid();
        var course = Domain.Models.Course.Create
        (id: courseId,
         name: createCourseCommand.Name!,
         description: createCourseCommand.Description!);

        return course;
    }
}
