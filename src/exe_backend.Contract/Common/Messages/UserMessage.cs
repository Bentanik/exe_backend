namespace exe_backend.Contract.Common.Messages;

public enum UserMessage
{
    [Message("Tạo tài khoản thành công", "user01")]
    CreateUserSuccessfully,

    [Message("Tài khoản này đã có gói đăng ký trước", "user02")]
    SubscriptionActivedException,

    [Message("Bạn đã kích hoạt Premium cho tài khoản thành công", "user03")]
    SubscriptionActivedSuccessfully,

    [Message("Bạn hiện tại không có kích hoạt Premium", "user04")]
    SubscriptionCancelFailException,

    [Message("Đã hủy thành công gói Premium", "user05")]
    SubscriptionCancelSuccessfully,

    [Message("Xin lỗi thời gian để gia hạn là dưới 10 ngày trở xuống", "user06")]
    SubscriptionNotRenewException,

    [Message("Bạn đã gia hạn gói Premium cho tài khoản thành công", "user07")]
    SubscriptionRenewSuccessfully,
}

