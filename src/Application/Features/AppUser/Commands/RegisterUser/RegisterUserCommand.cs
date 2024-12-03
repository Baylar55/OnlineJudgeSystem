namespace AlgoCode.Application.Features.AppUser.Commands.RegisterUser;

public record RegisterUserCommand(string Email, string UserName, string Password, string ConfirmPassword) : IRequest<ValidationResultModel>;
