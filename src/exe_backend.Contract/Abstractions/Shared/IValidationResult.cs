namespace exe_backend.Contract.Abstractions.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new
        ("ValidationError",
        "A Validation problem occured.");
    Error[] Errors { get; }
}
