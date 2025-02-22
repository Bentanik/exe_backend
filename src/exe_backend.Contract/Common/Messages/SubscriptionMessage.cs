namespace exe_backend.Contract.Common.Messages;

public enum SubscriptionMessage
{
    [Message("Đã tạo gói đăng ký thành công", "subscription01")]
    CreateSubscriptionMessageSuccessfully,

    [Message("Đã lấy gói đăng ký thành công", "subscription02")]
    GetSubscriptionMessageSuccessfully,

    [Message("Không tìm thấy gói đăng ký, xin vui lòng thử lại", "subscription03")]
    GetSubscriptionNotFoundException,

}

