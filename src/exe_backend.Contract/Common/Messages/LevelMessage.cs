namespace exe_backend.Contract.Common.Messages;

public enum LevelMessage
{
    [Message("Tạo cấp độ thành công", "level01")]
    CreateLevelSuccessfully,

    [Message("Lấy thành công cấp độ", "level02")]
    GetLevelSuccessfully
}

