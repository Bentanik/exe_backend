using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Presentation.Apis;

public static class CourseApi
{
    private const string BaseUrl = "/api/course/v{version:apiVersion}";
    public static IVersionedEndpointRouteBuilder MapCourseApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("create-course", HandleCreateCourseAsync).RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapPost("create-chapter", HandleCreateChapterAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapPost("create-lecture", HandleCreateLectureAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapGet("get-courses", HandleGetCoursesAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapGet("get-course-by-id", HandleGetCourseByIdAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapGet("get-chapters", HandleGetChaptersAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapGet("get-chapter-by-id", HandleGetChapterByIdAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapGet("get-lectures", HandleGetLecturesAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        group.MapGet("get-lecture-by-id", HandleGetLectureByIdAsync)
        .RequireAuthorization(RoleEnum.Admin.ToString());
        return builder;
    }

    private static async Task<IResult> HandleCreateCourseAsync(ISender sender, HttpContext context)
    {
        if (!context.Request.HasFormContentType)
        {
            return Results.BadRequest(new { Error = "Incorrect Content-Type. Expected 'multipart/form-data'." });
        }
        var form = await context.Request.ReadFormAsync();

        var name = form["Name"];
        var description = form["Description"];
        var thumbnailFile = form.Files["ThumbnailFile"];
        var categoryId = form["CategoryId"];
        var levelId = form["LevelId"];

        _ = Guid.TryParse(categoryId, out Guid categoryIdParsed);

        _ = Guid.TryParse(levelId, out Guid levelIdParsed);

        var request = new Command.CreateCourseCommand(name, description, thumbnailFile, categoryIdParsed, levelIdParsed);

        var result = await sender.Send(request);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateChapterAsync(ISender sender, [FromBody] Command.CreateChapterCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateLectureAsync(ISender sender, HttpContext context)
    {
        if (!context.Request.HasFormContentType)
        {
            return Results.BadRequest(new { Error = "Incorrect Content-Type. Expected 'multipart/form-data'." });
        }
        var form = await context.Request.ReadFormAsync();

        var lectureDto = new LectureDTO
        {
            Name = form["Name"],
            ChapterId = string.IsNullOrWhiteSpace(form["ChapterId"]) ? null : Guid.Parse(form["ChapterId"]),
            Description = form["Description"]
        };

        var imageFile = form.Files["ImageFile"];
        var videoFile = form.Files["VideoFile"];

        var request = new Command.CreateLectureCommand(lectureDto, imageFile!, videoFile!);

        var result = await sender.Send(request);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetCoursesAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetCoursesQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetCourseByIdAsync(ISender sender, [FromQuery] string courseId, [FromQuery] string[]? includes = null)
    {
        Guid? courseIdParsed = null;
        if (!string.IsNullOrEmpty(courseId))
        {
            if (!Guid.TryParse(courseId, out var parsedId))
            {
                return Results.BadRequest("Invalid course ID format.");
            }
            courseIdParsed = parsedId;
        }

        var result = await sender.Send(new Query.GetCourseByIdQuery(courseIdParsed, includes));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetLectureByIdAsync(ISender sender, [FromQuery] string lectureId, [FromQuery] string[]? includes = null)
    {
        Guid? lectureIdParsed = null;
        if (!string.IsNullOrEmpty(lectureId))
        {
            if (!Guid.TryParse(lectureId, out var parsedId))
            {
                return Results.BadRequest("Invalid lecture ID format.");
            }
            lectureIdParsed = parsedId;
        }

        var result = await sender.Send(new Query.GetLectureByIdQuery(lectureIdParsed, includes));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetChaptersAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetChaptersQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetChapterByIdAsync(ISender sender, [FromQuery] string chapterId, [FromQuery] string[]? includes = null)
    {
        Guid? chapterIdParsed = null;
        if (!string.IsNullOrEmpty(chapterId))
        {
            if (!Guid.TryParse(chapterId, out var parsedId))
            {
                return Results.BadRequest("Invalid course ID format.");
            }
            chapterIdParsed = parsedId;
        }

        var result = await sender.Send(new Query.GetChapterByIdQuery(chapterIdParsed, includes));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetLecturesAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetLecturesQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize));
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
