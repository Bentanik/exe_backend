namespace exe_backend.Contract.Common.Messages;

public enum AuthMessage
{
    [Message("Email này đã tồn tại", "em01")]
    EmailExistException,

    [Message("Đăng ký thành công", "auth01")]
    RegisterSuccessfully,
}

