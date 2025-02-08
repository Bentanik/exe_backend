using exe_backend.Contract.Services.Course;

namespace exe_backend.Presentation.Apis;

public static class CourseApi
{
    private const string BaseUrl = "/api/course/v{version:apiVersion}";
    public static IVersionedEndpointRouteBuilder MapCourseApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("create-course", CreateCourseAsync);
        return builder;
    }

    private static async Task<IResult> CreateCourseAsync(ISender sender, HttpContext context)
    {
        if (!context.Request.HasFormContentType)
        {
            return Results.BadRequest(new { Error = "Incorrect Content-Type. Expected 'multipart/form-data'." });
        }
        var form = await context.Request.ReadFormAsync();

        var name = form["Name"];
        var description = form["Description"];
        var thumbnailFile = form.Files["ThumbnailFile"];

        var request = new Command.CreateCourseCommand(name, description, thumbnailFile);

        var result = await sender.Send(request);

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
