﻿namespace exe_backend.Contract.Exceptions;

public abstract class AuthorizeException : DomainException
{
    protected AuthorizeException(string message, string? errorCode = null)
        : base("Unauthorized", message, errorCode)
    {
    }
}
