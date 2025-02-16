namespace exe_backend.Contract.Settings;

public class ClientSetting
{
    public const string SectionName = "ClientSetting";
    public string Url { get; set; } = default!;
    public string PurcharseSuccess { get; set; } = default!;
    public string PurcharseFail { get; set; } = default!;
}

