using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.AppUser.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<RegisterUserCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? dbEmail = await userManager.FindByEmailAsync(request.Email);
        ApplicationUser? dbUserName = await userManager.FindByNameAsync(request.UserName);

        var validationResult = new ValidationResultModel();

        if (dbUserName != null)
        {
            validationResult.Errors.Add("UserName", ["This username is already exist."]);
            return validationResult;
        }

        if (dbEmail != null)
        {
            validationResult.Errors.Add("Email", ["This email is already exist."]);
            return validationResult;
        }

        var user = request.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            validationResult.Errors.Add(string.Empty, result.Errors.Select(e => e.Description).ToList());
            return validationResult;
        }

        Session defaultSession = new()
        {
            Title = "Default",
            UserId = user.Id,
            IsActive = true
        };

        await context.Sessions.AddAsync(defaultSession, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
