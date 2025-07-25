using MediatR;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
