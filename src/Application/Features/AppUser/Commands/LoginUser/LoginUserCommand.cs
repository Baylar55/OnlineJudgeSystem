namespace AlgoCode.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ValidationResultModel>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
