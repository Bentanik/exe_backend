using exe_backend.Contract.DTOs.CategoryDTOs;

namespace exe_backend.Contract.Services.Category;

public static class Response
{
    public record CategoryResponse
        (CategoryDTO Category);

    public record CategoriesResponse
     (PagedResult<CategoryDTO> Categories);
}
