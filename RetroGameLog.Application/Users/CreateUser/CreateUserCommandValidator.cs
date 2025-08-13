using FluentValidation;

namespace RetroGameLog.Application.Users.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email.Value)
            .NotEmpty()
            .WithMessage("Email cannot be blank")
            .EmailAddress()
            .WithMessage("Email must be in correct format!");

        RuleFor(x => x.Username.Value)
            .NotEmpty()
            .WithMessage("Username cannot be blank")
            .Length(20)
            .WithMessage("Username length cannot be more than 20 characters");

        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.RegisteredAt).NotEmpty().Equal(DateTime.Now);
    }
}
