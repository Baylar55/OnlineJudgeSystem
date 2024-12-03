namespace AlgoCode.Application.Features.AppUser.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(p => p.Username)
            .NotEmpty().WithMessage("Username is required.");
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must not be less than 8 characters.");
    }
}
