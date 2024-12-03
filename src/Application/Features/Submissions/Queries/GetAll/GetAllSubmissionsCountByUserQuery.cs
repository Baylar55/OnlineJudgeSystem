using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Submissions.Queries.GetAll;

public record GetAllSubmissionsCountByUserQuery : IRequest<int>;

public class GetAllSubmissionsCountByUserQueryHandler(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<GetAllSubmissionsCountByUserQuery, int>
{
    public async Task<int> Handle(GetAllSubmissionsCountByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var user = await userManager.FindByIdAsync(userId!);

        return await context.Submissions
                             .Where(s => s.UserId == user!.Id)
                             .CountAsync(cancellationToken);
    }
}
