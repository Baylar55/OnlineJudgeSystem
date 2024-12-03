using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Solutions.Queries.GetAll;

public record GetAllSolutionsCountByUserQuery : IRequest<int>;

public class GetAllSolutionsQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<GetAllSolutionsCountByUserQuery, int>
{
    public async Task<int> Handle(GetAllSolutionsCountByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var user = await userManager.FindByIdAsync(userId!);

        var solutions = await context.Solutions
                             .Where(s => s.Submission.UserId == user!.Id)
                             .CountAsync(cancellationToken);
        return solutions;
    }
}
