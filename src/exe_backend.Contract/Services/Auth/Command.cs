namespace exe_backend.Contract.Services.Auth;

public static class Command
{
    public record RegisterCommand(string Email, string Password) : ICommand;
}