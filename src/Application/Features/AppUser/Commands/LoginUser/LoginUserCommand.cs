namespace AlgoCode.Application.Features.AppUser.Commands.LoginUser;

public record LoginUserCommand(string Username, string Password, string? ReturnUrl) : IRequest<ValidationResultModel>;
