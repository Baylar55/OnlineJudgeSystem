using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.AppUser.Commands.LoginAdmin
{
    public class LoginAdminCommand: IRequest<ValidationResultModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginAdminCommandHandler: IRequestHandler<LoginAdminCommand, ValidationResultModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginAdminCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ValidationResultModel> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add(string.Empty, new List<string> { "Username or password is incorrect." });
                return validationResult;
            }

            if(!await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()))
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
