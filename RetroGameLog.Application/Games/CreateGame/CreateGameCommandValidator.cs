using FluentValidation;

namespace RetroGameLog.Application.Games.CreateGame;

public sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(x => x.Developer)
            .NotEmpty()
            .WithMessage("Developer field cannot be blank");

        RuleFor(x => x.ReleaseYear)
            .NotEmpty()
            .WithMessage("Release year field cannot be blank");

        RuleFor(x => x.Genre)
            .NotEmpty()
            .WithMessage("Genre cannot be blank");

        RuleFor(X => X.Title)
            .NotEmpty()
            .WithMessage("Title cannot be blank");
    }
}
