using exe_backend.Contract.Services.Auth;

namespace exe_backend.Presentation;

public static class AuthApi
{
    private static readonly string BaseUrl = "/api/auth/v{version:apiVersion}";

    public static IVersionedEndpointRouteBuilder MapAuthApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("register", HandleRegisterAsync);
        // group.MapPost("verify-account", VerifyAccountAsync);
        // group.MapPost("login", LoginAsync);
        // group.MapGet("refresh-token", RefreshTokenAsync);

        return builder;
    }


    private static async Task<IResult> HandleRegisterAsync(ISender sender, [FromBody] Command.RegisterCommand command)
    {
        var result = await sender.Send(command);
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