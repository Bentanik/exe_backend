using exe_backend.Contract.Services.Course;

namespace exe_backend.Application.UseCases.V1.Commands.Course;

public sealed class CreateCourseCommandHandler
    : ICommandHandler<Command.CreateCourseCommand>
{
    public Task<Result> Handle(Command.CreateCourseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
