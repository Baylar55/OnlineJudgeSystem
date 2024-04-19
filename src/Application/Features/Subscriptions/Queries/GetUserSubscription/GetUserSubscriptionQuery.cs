using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Subscriptions.Queries.GetUserSubscription
{
    public class GetUserSubscriptionQuery:IRequest<SubscriptionStatus>
    {
        public string UserId { get; set; }
    }

    public class GetUserSubscriptionQueryHandler : IRequestHandler<GetUserSubscriptionQuery, SubscriptionStatus>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserSubscriptionQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<SubscriptionStatus> Handle(GetUserSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var status = user.SubscriptionStatus;
            return user.SubscriptionStatus;
        }
    }
}
