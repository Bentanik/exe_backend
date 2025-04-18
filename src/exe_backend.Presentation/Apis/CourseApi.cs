using exe_backend.Contract.Abstractions.Shared;
using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Presentation.Apis;

public static class CourseApi
{
    private const string BaseUrl = "/api/course/v{version:apiVersion}";
    public static IVersionedEndpointRouteBuilder MapCourseApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        // Role Admin
        group.MapPost("create-course", HandleCreateCourseAsync);

        group.MapPost("create-chapter", HandleCreateChapterAsync);

        group.MapPost("create-lecture", HandleCreateLectureAsync);

        group.MapGet("get-courses", HandleGetCoursesAsync);

        group.MapGet("get-course-by-id", HandleGetCourseByIdAsync);

        group.MapGet("get-chapters", HandleGetChaptersAsync);

        group.MapGet("get-chapter-by-id", HandleGetChapterByIdAsync);

        group.MapGet("get-lectures", HandleGetLecturesAsync);

        group.MapGet("get-lecture-by-id", HandleGetLectureByIdAsync);

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
        var chapterIds = form["ChapterId"].Select(Guid.Parse).ToArray();

        _ = Guid.TryParse(categoryId, out Guid categoryIdParsed);

        _ = Guid.TryParse(levelId, out Guid levelIdParsed);

        var request = new Command.CreateCourseCommand(name, description, thumbnailFile, categoryIdParsed, levelIdParsed, chapterIds);

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
            VideoLecture = new Contract.DTOs.MediaDTOs.VideoDTO(form["PublicVideoId"], Double.Parse(form["DurationVideo"])),
            Description = form["Description"]
        };

        var imageFile = form.Files["ImageFile"];

        var request = new Command.CreateLectureCommand(lectureDto, imageFile!);

        var result = await sender.Send(request);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);

        return Results.Ok("123");
    }

    private static async Task<IResult> HandleGetCoursesAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetCoursesQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetCourseByIdAsync(ISender sender,
    HttpContext context, [FromQuery] string courseId, [FromQuery] string[]? includes = null)
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

        _ = Guid.TryParse(context.User.FindFirstValue(AuthConstant.UserId), out Guid userId);

        var result = await sender.Send(new Query.GetCourseByIdQuery(courseIdParsed, includes, userId == Guid.Empty ? null : userId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetLectureByIdAsync(ISender sender, HttpContext context, [FromQuery] string lectureId, [FromQuery] string[]? includes = null)
    {
        _ = Guid.TryParse(context.User.FindFirstValue(AuthConstant.UserId), out Guid userId);

        Guid? lectureIdParsed = null;
        if (!string.IsNullOrEmpty(lectureId))
        {
            if (!Guid.TryParse(lectureId, out var parsedId))
            {
                return Results.BadRequest("Invalid lecture ID format.");
            }
            lectureIdParsed = parsedId;
        }

        var result = await sender.Send(new Query.GetLectureByIdQuery(userId,lectureIdParsed, includes));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetChaptersAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null, bool? NoneAssignedCourse = false)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetChaptersQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize, NoneAssignedCourse));
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

    private static async Task<IResult> HandleGetLecturesAsync(ISender sender, [FromQuery] string? searchTerm = null, [FromQuery] string? sortColumn = null, [FromQuery] string? sortOrder = null, int pageIndex = 1, int pageSize = 10, [FromQuery] string[]? includes = null, bool? NoneAssignedChapter = false)
    {
        var sort = !string.IsNullOrWhiteSpace(sortOrder) ? sortOrder.Equals("Asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;

        var result = await sender.Send(new Query.GetLecturesQuery(searchTerm, sortColumn, sort, includes, pageIndex, pageSize, NoneAssignedChapter));
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
