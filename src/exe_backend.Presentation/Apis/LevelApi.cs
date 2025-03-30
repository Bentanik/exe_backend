using exe_backend.Contract.Abstractions.Shared;
using exe_backend.Contract.Services.Level;

namespace exe_backend.Presentation.Apis;

public static class LevelApi
{
    private static readonly string BaseUrl = "/api/level/v{version:apiVersion}";

    public static IVersionedEndpointRouteBuilder MapLevelApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("create-level", HandleCreateLevelAsync);
        group.MapGet("get-levels", HandleGetLevelsAsync);
        group.MapGet("get-level-by-id", HandleGetLevelByIdAsync);
        return builder;
    }

    private static async Task<IResult> HandleCreateLevelAsync(ISender sender, [FromBody] Command.CreateLevelCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetLevelsAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetLevelsQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetLevelByIdAsync(ISender sender, [FromQuery] string levelId, [FromQuery] string[]? includes = null)
    {
        Guid? levelIdParsed = null;
        if (!string.IsNullOrEmpty(levelId))
        {
            if (!Guid.TryParse(levelId, out var parsedId))
            {
                return Results.BadRequest("Invalid level ID format.");
            }
            levelIdParsed = parsedId;
        }

        var result = await sender.Send(new Query.GetLevelByIdQuery(levelIdParsed, includes));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static IResult HandlerFailure(Result result) =>
         result switch
         {
             { IsSuccess: true } => throw new InvalidOperationException(),
             IValidationResult validationResult =>
                 Results.BadRequest(
                     CreateProblemDetails(
                         "Validation Error", StatusCodes.Status400BadRequest,
                         result.Error,
                         validationResult.Errors)),
             _ =>
                 Results.BadRequest(
                     CreateProblemDetails(
                         "Bab Request", StatusCodes.Status400BadRequest,
                         result.Error))
         };


    private static ProblemDetails CreateProblemDetails(
       string title,
       int status,
       Error error,
       Error[]? errors = null) =>
       new()
       {
           Title = title,
           Type = error.Code,
           Detail = error.Message,
           Status = status,
           Extensions = { { nameof(errors), errors } }
       };
}