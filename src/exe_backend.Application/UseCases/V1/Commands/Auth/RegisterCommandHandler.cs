using exe_backend.Contract.Common.Enums;
using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Commands.Auth;

public sealed class RegisterCommandHandler
    (IPasswordHashService passwordHashService, IUnitOfWork unitOfWork, IPublisher publisher, IOptions<UserSetting> userSetting)
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

        // Get avatarDto from appsetting
        var avatarDto = userSetting.Value.Avatar;
        // Password hash
        var passwordHashed = passwordHashService.HashPassword(command.Password);

        // Create user and save Db
    
        // Find Role
        var roleMember = await unitOfWork.RoleRepository
            .FindSingleAsync(r => r.Name == RoleEnum.Member.ToString());

        var userId = Guid.NewGuid();
        var user = User.Create(userId, command.Email, passwordHashed, command.FullName, roleMember.Id, avatarDto.AvatarId, avatarDto.AvatarUrl);

        unitOfWork.UserRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send event when user registered
        var userDto = user.Adapt<UserDto>();

        await publisher.Publish(new Event.UserRegisterdEvent(Guid.NewGuid(), userDto), cancellationToken);

        return Result.Success(new Success
        (AuthMessage.RegisterSuccessfully.GetMessage().Code,
        AuthMessage.RegisterSuccessfully.GetMessage().Message));
    }
}
