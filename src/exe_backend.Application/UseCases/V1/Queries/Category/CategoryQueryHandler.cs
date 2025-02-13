using exe_backend.Contract.DTOs.CategoryDTOs;
using exe_backend.Contract.Services.Category;

namespace exe_backend.Application.UseCases.V1.Queries.Category;

public sealed class CategoryQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetCategoriesQuery, Success<Contract.Services.Category.Response.CategoriesResponse>>
{
    public async Task<Result<Success<Contract.Services.Category.Response.CategoriesResponse>>> Handle(Query.GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
       {
           return include switch
           {
               "Course" => (Expression<Func<Domain.Models.Category, object>>)(ct => ct.Courses),
               _ => throw new ArgumentException($"Unknown navigation property: {include}")
           };
       }).ToArray();

        //Find sort property without Id
        var categoriesQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.CategoryRepository.FindAll(includeProperties: includes) : unitOfWork.CategoryRepository.FindAll(x => x.Name.Contains(query.SearchTerm), includeProperties: includes);

        // Get sort follow property
        Expression<Func<Domain.Models.Category, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            "name" => category => category.Name,
            _ => category => category.CreatedDate!,
        };

        categoriesQuery = query.SortOrder == SortOrder.Descending
             ? categoriesQuery.OrderByDescending(keySelector) : categoriesQuery.OrderBy(keySelector);

        var pagedResultCourse = await PagedResult<Domain.Models.Category>.CreateAsync(categoriesQuery, query.PageIndex, query.PageSize);

        var categoryDtos = pagedResultCourse.Items.Adapt<List<CategoryDTO>>();

        var pagedResultCategoryDto = PagedResult<CategoryDTO>.Create(categoryDtos, pagedResultCourse.PageIndex, pagedResultCourse.PageSize, pagedResultCourse.TotalCount);

        var response = new Contract.Services.Category.Response.CategoriesResponse(pagedResultCategoryDto);

        return Result.Success(new Success<Contract.Services.Category.Response.CategoriesResponse>(CourseMessage.GetCourseSuccessfully.GetMessage().Code, CourseMessage.GetCourseSuccessfully.GetMessage().Message, response));
    }
}
