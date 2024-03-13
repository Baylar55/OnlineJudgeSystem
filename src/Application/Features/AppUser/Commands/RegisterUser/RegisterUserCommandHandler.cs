namespace AlgoCode.Application.Features.AppUser.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ValidationResultModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ValidationResultModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dbEmail = await _userManager.FindByEmailAsync(request.Email);
            var dbUserName = await _userManager.FindByNameAsync(request.UserName);
            var validationResult = new ValidationResultModel();
            if (dbEmail != null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Email", new List<string> { "This email is already exist." });
                return validationResult;
            }

            if (dbUserName != null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("UserName", new List<string> { "This username is already exist." });
                return validationResult;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add(string.Empty, result.Errors.Select(e => e.Description).ToList());
                return validationResult;
            }

            return validationResult;
        }
    }
}
