﻿namespace exe_backend.Contract.Exceptions ;
public abstract class NotFoundException : DomainException
{
    protected NotFoundException(string message, string? errorCode = null)
        : base("Not Found", message, errorCode)
    {
    }
}
