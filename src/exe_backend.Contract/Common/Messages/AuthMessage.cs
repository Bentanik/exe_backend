namespace exe_backend.Contract.Common.Messages;

public enum AuthMessage
{
    [Message("Email này đã tồn tại", "auth01")]
    EmailExistException,

    [Message("Email này chưa tồn tại", "auth02")]
    EmailNotExistException,

    [Message("Đăng ký thành công, xác nhận tài khoản được gửi vào mail", "auth03")]
    RegisterSuccessfully,

    [Message("Mật khẩu không trùng khớp", "auth04")]
    PasswordNotMatchException,

    [Message("Đăng nhập thành công", "auth05")]
    LoginSuccessfully,

    [Message("Xin vui lòng vô Gmail để kiểm tra", "auth06")]
    ConfirmForgotPasswordSuccessfully
}

