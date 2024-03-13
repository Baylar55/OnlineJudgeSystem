namespace AlgoCode.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ValidationResultModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ValidationResultModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add(string.Empty, new List<string> { "Username or password is incorrect." });
                return validationResult;
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add(string.Empty, new List<string> { "Username or password is incorrect." });
                return validationResult;
            }
            return validationResult;
        }
    }
}
