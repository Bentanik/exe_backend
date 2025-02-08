namespace exe_backend.Contract.Services.Course.Validators;

public sealed class CreateCourseValidator
    : AbstractValidator<Command.CreateCourseCommand>
{
    public CreateCourseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .MinimumLength(3).WithMessage("Course name must be at least 3 characters long.")
                    .MaximumLength(250).WithMessage("Course name cannot be longer than 250 characters.");
            });

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Description)
                    .MinimumLength(3).WithMessage("Description must be at least 3 characters long.");
            });
        
        RuleFor(x => x.ThumbnailFile)
            .NotNull().WithMessage("Thumbnail file cannot be null.")
            .DependentRules(() =>
            {
                RuleFor(x => x.ThumbnailFile)
                    .Must(file => file.Length > 0).WithMessage("Thumbnail file cannot be empty.")
                    .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Thumbnail must be an image file (jpg, png, jpeg, etc.).")
                    .Must(file => file.Length <= 5 * 1024 * 1024) // Max 5MB
                    .WithMessage("Thumbnail file cannot be larger than 5MB.");
            });
    }
}