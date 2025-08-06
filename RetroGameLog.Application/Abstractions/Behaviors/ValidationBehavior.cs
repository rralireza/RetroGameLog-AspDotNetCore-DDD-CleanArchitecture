﻿using FluentValidation;
using MediatR;
using RetroGameLog.Application.Abstractions.Exceptions;
using RetroGameLog.Application.Abstractions.Messaging;

namespace RetroGameLog.Application.Abstractions.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(x => x.Validate(context))
                .Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .Select(x => new ValidationErrors(x.ErrorCode, x.PropertyName, x.ErrorMessage))
                .ToList();


            if (validationErrors.Any())
                throw new Exceptions.ValidationException(validationErrors);

            return await next();
        }

        return await next();

    }
}
