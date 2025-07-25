using MediatR;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}


public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
