namespace exe_backend.Contract.Services.Auth.Validators;

public sealed class ChangePasswordValidator 
    : AbstractValidator<Command.ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .DependentRules(() =>
            {
                RuleFor(x => x.NewPassword)
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                    .MaximumLength(100).WithMessage("Password cannot be longer than 100 characters.");
            });
    }
}