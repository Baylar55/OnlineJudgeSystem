using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.AppUser.Commands.LoginAdmin;

public record LoginAdminCommand(string Username, string Password): IRequest<ValidationResultModel>;

public class LoginAdminCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IRequestHandler<LoginAdminCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();
        var user = await userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            validationResult.Errors.Add(string.Empty, ["Username or password is incorrect."]);
            return validationResult;
        }

        if(!await userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()))
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
