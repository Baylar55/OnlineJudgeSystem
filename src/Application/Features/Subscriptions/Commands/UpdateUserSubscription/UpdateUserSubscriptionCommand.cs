using Microsoft.AspNetCore.Http;

namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateUserSubscription
{
    public class UpdateUserSubscriptionCommand : IRequest<Unit>
    {
        public int SubscriptionId { get; set; }
        public SubscriptionStatus SubscriptionStatus { get; set; }
    }

    public class UpdateUserSubscriptionCommandHandler : IRequestHandler<UpdateUserSubscriptionCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserSubscriptionCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Unit> Handle(UpdateUserSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            user.SubscriptionId = request.SubscriptionId;
            user.SubscriptionStatus = request.SubscriptionStatus;
            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
