namespace exe_backend.Contract.Services.Auth;

public static class Command
{
    public record RegisterCommand(string Email, string Password, string FullName) : ICommand;

    public record ConfirmForgotPasswordCommand(string Email) : ICommand;

    public record ChangePasswordCommand(string TokenVerify, string NewPassword) : ICommand;
}