namespace AlgoCode.Application.Features.AppUser.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<ValidationResultModel>
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
