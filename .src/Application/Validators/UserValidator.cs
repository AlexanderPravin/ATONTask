using FluentValidation;

namespace Application.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Matches("^[a-zA-Zа-яА-Я]*$")
            .WithMessage("Incorrect name");
        
        RuleFor(x => x.Login)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Incorrect login");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Incorrect password");
    }
}