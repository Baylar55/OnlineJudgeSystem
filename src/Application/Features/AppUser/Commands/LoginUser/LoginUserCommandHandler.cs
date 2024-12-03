using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.AppUser.Commands.LoginUser;

public class LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IRequestHandler<LoginUserCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();
        var user = await userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            validationResult.Errors.Add(string.Empty, ["Username or password is incorrect."]);
            return validationResult;
        }
        
        var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
        
        if (!result.Succeeded)
        {
            validationResult.Errors.Add(string.Empty, ["Username or password is incorrect."]);
            return validationResult;
        }
        
        return validationResult;
    }
}
