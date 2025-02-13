using exe_backend.Contract.Services.Category;

namespace exe_backend.Application.UseCases.V1.Commands.Category;

public sealed class CreateCategoryCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.CreateCategoryCommand>
{
    public async Task<Result> Handle(Command.CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryId = Guid.NewGuid();
        var category = Domain.Models.Category.Create(categoryId, command.Name);

        // Save database
        unitOfWork.CategoryRepository.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(CategoryMessage.CreateCategorySuccessfully.GetMessage().Code, CategoryMessage.CreateCategorySuccessfully.GetMessage().Message));
    }
}
