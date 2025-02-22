using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;
using Firebase.Auth;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class RegisterCommandHandler
    (ISender sender, IFirebaseAuthService firebaseAuthService)
    : ICommandHandler<Command.RegisterCommand>
{
    public async Task<Result> Handle(Command.RegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var registerFirebase = await firebaseAuthService.RegisterWithEmailAndPasswordAsync(command.Email, command.Password);

            // Send to service create user
            var createUserCommand = MapToCreateUserCommand(command, registerFirebase.User.Uid);

            _ = await sender.Send(createUserCommand, cancellationToken);

            return Result.Success(new Success
            (AuthMessage.RegisterSuccessfully.GetMessage().Code,
            AuthMessage.RegisterSuccessfully.GetMessage().Message));
        }
        catch (FirebaseAuthHttpException ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
            throw new AuthException.UserExistException();
        }
    }

    private static Contract.Services.User.Command.CreateUserCommand MapToCreateUserCommand(Command.RegisterCommand command, string identityId)
    {
        var userId = Guid.NewGuid();
        var userDto = new UserDto(Id: userId, IdentityId: identityId, Email: command.Email, FullName: command.FullName);

        var createUserCommand = new Contract.Services.User.Command.CreateUserCommand(userDto);

        return createUserCommand;
    }
}
