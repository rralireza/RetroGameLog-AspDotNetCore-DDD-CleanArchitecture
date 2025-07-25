﻿using MediatR;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}
