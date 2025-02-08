namespace exe_backend.Contract.Services.Auth.Validators;

public sealed class ConfirmForgotPasswordValidator 
    : AbstractValidator<Command.ConfirmForgotPasswordCommand>
{
    public ConfirmForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("Invalid email format.")
                    .DependentRules(() =>
                    {
                        RuleFor(x => x.Email)
                            .MinimumLength(5).WithMessage("Email must be at least 2 characters long.")
                            .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");
                    });
            });
    }
}