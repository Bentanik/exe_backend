using exe_backend.Contract.Services.Level;

namespace exe_backend.Application.UseCases.V1.Commands.Level;

public sealed class CreateLevelCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.CreateLevelCommand>
{
    public async Task<Result> Handle(Command.CreateLevelCommand command, CancellationToken cancellationToken)
    {
        var levelId = Guid.NewGuid();
        var level = Domain.Models.Level.Create(levelId, command.Name);

        // Save database
        unitOfWork.LevelRepository.Add(level);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(LevelMessage.CreateLevelSuccessfully.GetMessage().Code, LevelMessage.CreateLevelSuccessfully.GetMessage().Message));
    }
}
