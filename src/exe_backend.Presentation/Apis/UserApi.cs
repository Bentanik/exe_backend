using exe_backend.Contract.Exceptions.BussinessExceptions;
using exe_backend.Contract.Services.User;

namespace exe_backend.Presentation.Apis;

public static class UserApi
{
    private const string BaseUrl = "/api/user/v{version:apiVersion}";
    public static IVersionedEndpointRouteBuilder MapUserApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        // Role User
        group.MapPost("purcharse-vip", HandlePurcharseVipAsync).RequireAuthorization(RoleEnum.Member.ToString());

        group.MapGet("success-purcharse-vip", HandleSuccessPurcharseVipAsync);

        group.MapGet("fail-purcharse-vip", HandleFailPurcharseVipAsync);

        group.MapPost("cancel-vip", HandleCancelVipAsync)
        .RequireAuthorization(RoleEnum.Member.ToString());

        group.MapPost("renew-vip", HandleRenewVipAsync)
        .RequireAuthorization(RoleEnum.Member.ToString());

        group.MapGet("get-info-user-by-id", HandleGetInfoUserAsync);

        group.MapGet("get-bill-by-id", HandleGetBillAsync);

        return builder;
    }

    private static async Task<IResult> HandlePurcharseVipAsync(ISender sender, HttpContext context, [FromQuery] string subscriptionPackageId)
    {
        _ = Guid.TryParse(context.User.FindFirstValue(AuthConstant.UserId), out Guid userId);

        if (userId == null || userId == Guid.Empty) throw new AuthException.TokenPasswordExpiredException();

        _ = Guid.TryParse(subscriptionPackageId, out Guid subscriptionPackageIdParsed);

        if (userId == null || userId == Guid.Empty) throw new Exception("Wrong format");

        var result = await sender.Send(new Command.PurcharseVipCommand(userId, subscriptionPackageIdParsed));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleSuccessPurcharseVipAsync(ISender sender, HttpContext context, [FromQuery] long orderId)
    {
        var result = await sender.Send(new Command.SuccessPurcharseVipCommand(orderId));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleFailPurcharseVipAsync(ISender sender, HttpContext context, [FromQuery] long orderId)
    {
        var result = await sender.Send(new Command.FailPurcharseVipCommand(orderId));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCancelVipAsync(ISender sender, HttpContext context)
    {
        _ = Guid.TryParse(context.User.FindFirstValue(AuthConstant.UserId), out Guid userId);

        if (userId == null || userId == Guid.Empty) throw new AuthException.TokenPasswordExpiredException();

        var result = await sender.Send(new Command.CancelVipCommand(userId));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleRenewVipAsync(ISender sender, HttpContext context)
    {
        _ = Guid.TryParse(context.User.FindFirstValue(AuthConstant.UserId), out Guid userId);

        if (userId == null || userId == Guid.Empty) throw new AuthException.TokenPasswordExpiredException();

        var result = await sender.Send(new Command.RenewVipCommand(userId));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetBillAsync(ISender sender, [FromQuery] string billId)
    {
        _ = Guid.TryParse(billId, out Guid billIdParase);

        var result = await sender.Send(new Query.GetBillByIdQuery(billIdParase));

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetInfoUserAsync(ISender sender, HttpContext context, [FromQuery] string userId, [FromQuery] string[]? includes = null)
    {
        // _ = Guid.TryParse(context.User.FindFirstValue(AuthConstant.UserId), out Guid userId);

        var result = await sender.Send(new Query.GetUserByIdQuery(Guid.Parse(userId), includes));

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
