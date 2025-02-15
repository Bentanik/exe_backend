namespace exe_backend.Contract.Common.Messages;

public enum SubscriptionMessage
{
    [Message("Đã tạo gói đăng ký thành công", "subscription01")]
    CreateSubscriptionMessageSuccessfully,

    [Message("Đã lấy gói đăng ký thành công", "subscription01")]
    GetSubscriptionMessageSuccessfully,
}

