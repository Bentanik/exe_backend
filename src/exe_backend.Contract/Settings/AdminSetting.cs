namespace exe_backend.Contract.Settings;

public class AdminSetting
{
    public const string SectionName = "AdminSetting";

    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FullName {get;set;} = default!;
}