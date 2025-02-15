namespace exe_backend.Contract.Common.Messages;

public enum UserMessage
{
    [Message("Tài khoản này đã có gói đăng ký trước", "user01")]
    SubscriptionActivedException,

    [Message("Bạn đã kích hoạt Premium cho tài khoản thành công", "user02")]
    SubscriptionActivedSuccessfully,

    [Message("Bạn hiện tại không có kích hoạt Premium", "user03")]
    SubscriptionCancelFailException,

    [Message("Đã hủy thành công gói Premium", "user04")]
    SubscriptionCancelSuccessfully,

    [Message("Xin lỗi thời gian để gia hạn là dưới 10 ngày trở xuống", "user05")]
    SubscriptionNotRenewException,

    [Message("Bạn đã gia hạn gói Premium cho tài khoản thành công", "user06")]
    SubscriptionRenewSuccessfully,
}

