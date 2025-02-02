using exe_backend.Contract.Shared;
using MediatR;

namespace exe_backend.Contract.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}