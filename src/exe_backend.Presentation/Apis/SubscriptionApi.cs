using exe_backend.Contract.Services.Subscription;

namespace exe_backend.Presentation.Apis;

public static class SubscriptionApi
{
    private static readonly string BaseUrl = "/api/subscription/v{version:apiVersion}";

    public static IVersionedEndpointRouteBuilder MapSubscriptionApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("create-subscription-package", HandleCreateSubscriptionPackageAsync).RequireAuthorization(RoleEnum.Admin.ToString());

        group.MapGet("get-subscription-packages", HandleGetSubscriptionPackagesAsync);

        group.MapGet("get-subscription-package-by-id", HandleGetSubscriptionByIdPackageAsync);
        return builder;
    }

    private static async Task<IResult> HandleCreateSubscriptionPackageAsync(ISender sender, [FromBody] Command.CreateSubscriptionPackageCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetSubscriptionPackagesAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetSubscriptionPackagesQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetSubscriptionByIdPackageAsync(ISender sender, [FromQuery] string subscriptionPackageId, [FromQuery] string[]? includes = null)
    {
        Guid? subscriptionPackageIdParsed = null;
        if (!string.IsNullOrEmpty(subscriptionPackageId))
        {
            if (!Guid.TryParse(subscriptionPackageId, out var parsedId))
            {
                return Results.BadRequest("Invalid level ID format.");
            }
            subscriptionPackageIdParsed = parsedId;
        }

        var result = await sender.Send(new Query.GetSubscriptionPackageByIdQuery(subscriptionPackageIdParsed, includes));
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