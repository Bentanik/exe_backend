namespace exe_backend.Contract.Settings;

public class UserSetting
{
    public const string SectionName = "UserSetting";
    public AvatarSetting Avatar { get; set; } = default!;
}
