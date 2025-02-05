using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class ConfirmForgotPasswordCommandHandler
    (IUnitOfWork unitOfWork, ITokenGeneratorService tokenGeneratorService, IPublisher publisher)
    : ICommandHandler<Command.ConfirmForgotPasswordCommand>
{
    public async Task<Result> Handle(Command.ConfirmForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await unitOfWork.UserRepository
            .FindSingleAsync(predicate: u => u.Email == command.Email,  cancellationToken: cancellationToken);

        // Check if user not exist
        if(user == null) throw new AuthException.UserNotExistException();

        var forgotPasswordToken = tokenGeneratorService.GenerateForgotPasswordToken(user.Id);
    
        // Send event when created forgotPasswordToken
        var userDto = new UserDto(Email: user.Email);

        var userPasswordResetConfirmedEvent = new Event.UserPasswordResetConfirmedEvent(Guid.NewGuid(), userDto, forgotPasswordToken);

        await publisher.Publish(userPasswordResetConfirmedEvent, cancellationToken);

        return Result.Success(new Success("", ""));
    }
}
