using exe_backend.Contract.Abstractions.Shared;
using exe_backend.Contract.Services.Donate;

namespace exe_backend.Presentation.Apis;

public static class DonateApi
{
    private const string BaseUrl = "/api/donate/v{version:apiVersion}";
    public static IVersionedEndpointRouteBuilder MapDonateApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        // Role User
        group.MapPost("donate", HandleDonateAsync);
        group.MapGet("success-donate", HandleSuccessDonateAsync);
        group.MapGet("fail-donate", HandleFailDonateAsync);


        return builder;
    }

    private static async Task<IResult> HandleDonateAsync(ISender sender, HttpContext context, [FromBody] Command.CreateDonateCommand request)
    {
        Guid userId = Guid.Empty;
        var userIdValue = context.User.FindFirstValue(AuthConstant.UserId);

        if (userIdValue != null)
        {
            userId = Guid.Parse(userIdValue);
        }

        var result = await sender.Send(new Command.CreateDonateCommand(request.Price, request.Description, userId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleSuccessDonateAsync(ISender sender, HttpContext context, [FromQuery] long orderId)
    {
        var result = await sender.Send(new Command.SuccessDonateCommand(orderId));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Redirect(result?.Value);
    }

    private static async Task<IResult> HandleFailDonateAsync(ISender sender, HttpContext context, [FromQuery] long orderId)
    {
        var result = await sender.Send(new Command.FailDonateCommand(orderId));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Redirect(result?.Value);
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
