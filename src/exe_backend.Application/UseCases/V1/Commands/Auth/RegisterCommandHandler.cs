using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;
using Mapster;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class RegisterCommandHandler
    (IPasswordHashService passwordHashService, IUnitOfWork unitOfWork, IPublisher publisher)
    : ICommandHandler<Command.RegisterCommand>
{
    public async Task<Result> Handle(Command.RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check user exists
        var userExist = await unitOfWork.UserRepository
            .AnyAsync(u => u.Email == command.Email);
        // If user exists will exception
        if (userExist == true)
            throw new AuthException.UserExistException();

        // Password hash
        var passwordHashed = passwordHashService.HashPassword(command.Password);

        // Create user and save Db
        var user = User.Create(Guid.NewGuid(), command.Email, passwordHashed, command.FullName);

        unitOfWork.UserRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send event when user registered
        var userDto = user.Adapt<UserDto>();
        
        await publisher.Publish(new Event.UserRegisterdEvent(Guid.NewGuid(), userDto), cancellationToken);

        return Result.Success(new Success("Okk", "This is register command"));
    }
}
