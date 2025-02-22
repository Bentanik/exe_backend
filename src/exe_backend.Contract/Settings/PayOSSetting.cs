namespace exe_backend.Contract.Settings;

public class PayOSSetting
{
    public const string SectionName = "PayOSSetting";
    public string ClientId { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public string ChecksumKey { get; set; } = default!;
    public string SuccessUrl { get; set; } = default!;
    public string ErrorUrl { get; set; } = default!;
}
