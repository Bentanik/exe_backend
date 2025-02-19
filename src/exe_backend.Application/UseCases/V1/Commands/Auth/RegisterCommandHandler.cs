using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class RegisterCommandHandler
    (ISender sender)
    : ICommandHandler<Command.RegisterCommand>
{
    public async Task<Result> Handle(Command.RegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Create account use firebase
            var userArgs = new UserRecordArgs
            {
                Email = command.Email,
                Password = command.Password,
            };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs, cancellationToken);

            // Send to service create user

            var createUserCommand = MapToCreateUserCommand(command, userRecord.Uid);

            _ = await sender.Send(createUserCommand, cancellationToken);

            return Result.Success(new Success
            (AuthMessage.RegisterSuccessfully.GetMessage().Code,
            AuthMessage.RegisterSuccessfully.GetMessage().Message));
        }
        catch (FirebaseAuthException ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
            throw new AuthException.UserExistException();
        }
    }

    private static Contract.Services.User.Command.CreateUserCommand MapToCreateUserCommand(Command.RegisterCommand command, string identityId)
    {
        var userId = Guid.NewGuid();
        var userDto = new UserDto(Id: userId, Email: command.Email, IdentityId: identityId, FullName: command.FullName);

        var createUserCommand = new Contract.Services.User.Command.CreateUserCommand(userDto);

        return createUserCommand;
    }
}
