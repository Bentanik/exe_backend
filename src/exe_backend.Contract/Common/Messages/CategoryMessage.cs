namespace exe_backend.Contract.Common.Messages;

public enum CategoryMessage
{
    [Message("Tạo thành công thể loại", "category01")]
    CreateCategorySuccessfully,

    [Message("Lấy thành công thể loại", "category02")]
    GetCategorySuccessfully
}

