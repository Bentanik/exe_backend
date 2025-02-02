using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class RegisterCommandHandler
    (IResponseCacheService responseCacheService, IUnitOfWork unitOfWork)
    : ICommandHandler<Command.RegisterCommand>
{
    public async Task<Result> Handle(Command.RegisterCommand command, CancellationToken cancellationToken)
    {
        // Test save redis
        await responseCacheService.SetCacheResponseAsync("Hehe", "Hehe", TimeSpan.FromSeconds(60));

        // Test save db
        var user = User.Create(Guid.NewGuid(), command.Email, command.Password);
        unitOfWork.UserRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        throw new Exception("HEHE");
        return Result.Success(new Success("Okk", "This is register command"));
    }
}
