using exe_backend.Contract.Common.Constants;
using exe_backend.Contract.Common.Messages;
using exe_backend.Contract.Services.Auth;
using exe_backend.Contract.Settings;
using Microsoft.Extensions.Options;

namespace exe_backend.Presentation.Apis;

public static class AuthApi
{
    private static readonly string BaseUrl = "/api/auth/v{version:apiVersion}";

    public static IVersionedEndpointRouteBuilder MapAuthApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("register", HandleRegisterAsync);
        group.MapPost("login", HandleLoginAsync);
        group.MapPost("refresh-token", HandleRefreshTokenAsync);
        group.MapPost("confirm-forgot-password", HandleConfirmForgotPasswordAsync);
        group.MapPost("change-password", HandleChangePasswordAsync);
        group.MapPost("logout", HandleLogout);
        return builder;
    }

    private static async Task<IResult> HandleRegisterAsync(ISender sender, [FromBody] Command.RegisterCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleLoginAsync(ISender sender, [FromBody] Query.LoginQuery request, HttpContext httpContext, IOptions<AuthSetting> AuthSetting)
    {
        var result = await sender.Send(request);

        if (result.IsFailure)
            return HandlerFailure(result);

        var value = result.Value;

        var refreshTokenExpMinute = AuthSetting.Value.RefreshTokenExpMinute;

        httpContext.Response.Cookies.Append(AuthConstant.RefreshToken,
            value.Data.LoginDto.AuthTokenDTO.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(refreshTokenExpMinute),
            });

        var loginDto = value.Data.LoginDto with
        {
            AuthTokenDTO = value.Data.LoginDto.AuthTokenDTO with
            {
                RefreshToken = null // Remove refresh token when return
            }
        };

        return Results.Ok(loginDto);
    }

    private static async Task<IResult> HandleRefreshTokenAsync
  (ISender sender, HttpContext httpContext, IOptions<AuthSetting> AuthSetting)
    {
        var refreshToken = httpContext.Request.Cookies[AuthConstant.RefreshToken];

        var result = await sender.Send(new Query.RefreshTokenQuery(refreshToken));

        if (result.IsFailure)
            return HandlerFailure(result);

        var value = result.Value;

        var refreshTokenExpMinute = AuthSetting.Value.RefreshTokenExpMinute;

        httpContext.Response.Cookies.Append(AuthConstant.RefreshToken,
            value.Data.LoginDto.AuthTokenDTO.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(refreshTokenExpMinute),
            });

        var loginDto = value.Data.LoginDto with
        {
            AuthTokenDTO = value.Data.LoginDto.AuthTokenDTO with
            {
                RefreshToken = null // Remove refresh token when return
            }
        };

        return Results.Ok(loginDto);
    }

    private static async Task<IResult> HandleConfirmForgotPasswordAsync(ISender sender, [FromBody] Command.ConfirmForgotPasswordCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleChangePasswordAsync(ISender sender, [FromBody] Command.ChangePasswordCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static IResult HandleLogout(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete(AuthConstant.RefreshToken);
        return Results.Ok(Result.Success(new Success(AuthMessage.LogoutSuccessfully.GetMessage().Code,
            AuthMessage.LogoutSuccessfully.GetMessage().Message)));
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