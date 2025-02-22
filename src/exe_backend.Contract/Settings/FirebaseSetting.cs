namespace exe_backend.Contract.Settings;

public class FirebaseSetting
{
    public const string SectionName = "FirebaseSetting";

    public string WebApiKey { get; set; } = default!;
    public string AuthDomain { get; set; } = default!;
}
