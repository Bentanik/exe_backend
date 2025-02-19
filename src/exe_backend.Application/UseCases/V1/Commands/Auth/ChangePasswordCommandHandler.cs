using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class ChangePasswordCommandHandler
    (ITokenGeneratorService tokenGeneratorService,
    IPasswordHashService passwordHashService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<Command.ChangePasswordCommand>
{
    public async Task<Result> Handle(Command.ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        // Decode token and get userId
        var userIdClaim = tokenGeneratorService.ValidateAndGetUserIdForgotPasswordToken(command.TokenVerify);

        // When the token is sold out or the token is wrong
        if (userIdClaim == null)
            throw new AuthException.TokenPasswordExpiredException();

        var userId = Guid.Parse(userIdClaim);

        var user = await unitOfWork.UserRepository.FindSingleAsync(u => u.Id == userId);
        
        // If not find user
        if(user == null)
            throw new AuthException.UserNotExistException();
        
        // Hash password
        var newHashPassword = passwordHashService.HashPassword(command.NewPassword);
        
        // Update user
        // user.Update(password: newHashPassword);

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success
        (AuthMessage.ChangePasswordSuccessfully.GetMessage().Code,
        AuthMessage.ChangePasswordSuccessfully.GetMessage().Message));
    }
}
