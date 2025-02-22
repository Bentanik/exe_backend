using exe_backend.Contract.DTOs.CategoryDTOs;
using exe_backend.Contract.Services.Category;

namespace exe_backend.Application.UseCases.V1.Queries.Category;

public sealed class GetCategoryByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetCategoryByIdQuery, Success<Contract.Services.Category.Response.CategoryResponse>>
{
    public async Task<Result<Success<Contract.Services.Category.Response.CategoryResponse>>> Handle(Query.GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
        {
            return include switch
            {
                "Course" => (Expression<Func<Domain.Models.Category, object>>)(c => c.Courses),
                _ => throw new ArgumentException($"Unknown navigation property: {include}")
            };
        }).ToArray();

        var result = await unitOfWork.CategoryRepository
            .FindSingleAsync(c => c.Id == query.CategoryId, cancellationToken, includes);

        var categoryDto = result.Adapt<CategoryDTO>();

        var response = new Contract.Services.Category.Response.CategoryResponse(categoryDto);

        return Result.Success(new Success<Contract.Services.Category.Response.CategoryResponse>(CategoryMessage.GetCategorySuccessfully.GetMessage().Code, CategoryMessage.GetCategorySuccessfully.GetMessage().Message, response));
    }
}
