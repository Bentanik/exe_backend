namespace exe_backend.Presentation.Apis;

public static class LevelApi
{
    private static readonly string BaseUrl = "/api/level/v{version:apiVersion}";

    public static IVersionedEndpointRouteBuilder MapLevelApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        // group.MapPost("create-level", HandleLevelAsync);
        return builder;
    }

    // private static async Task<IResult> HandleLevelAsync(ISender sender, [FromBody] Command.RegisterCommand request)
    // {
    //     var result = await sender.Send(request);
    //     if (result.IsFailure)
    //         return HandlerFailure(result);

    //     return Results.Ok(result);
    // }


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