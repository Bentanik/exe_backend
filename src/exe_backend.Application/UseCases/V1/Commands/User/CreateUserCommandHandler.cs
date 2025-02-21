using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class CreateUserCommandHandler
    (IOptions<UserSetting> userSetting, IUnitOfWork unitOfWork, IPublisher publisher)
    : ICommandHandler<Command.CreateUserCommand>
{
    public async Task<Result> Handle(Command.CreateUserCommand command, CancellationToken cancellationToken)
    {
        // Find role
        var roleMember = await unitOfWork.RoleRepository
            .FindSingleAsync(r => r.Name == RoleEnum.Member.ToString());

        var user = MapToUser(command.UserDto, roleMember, userSetting.Value.Avatar);

        // Save db
        unitOfWork.UserRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send event when user registered
        await publisher.Publish(new Contract.Services.Auth.Event.UserRegisterdEvent(Guid.NewGuid(), command.UserDto), cancellationToken);

        return Result.Success(new Success(UserMessage.CreateUserSuccessfully.GetMessage().Code, UserMessage.CreateUserSuccessfully.GetMessage().Message));
    }

    private static Domain.Models.User MapToUser(UserDto userDto, Role role, AvatarSetting avatarSetting)
    {
        var user = Domain.Models.User.Create
        (id: (Guid)userDto.Id!,
         email: userDto.Email!,
         fullName: userDto.FullName!,
         identityId: userDto.IdentityId!,
         roleId: role.Id,
         avatarSetting.AvatarId,
         avatarSetting.AvatarUrl);

        return user;
    }
}
