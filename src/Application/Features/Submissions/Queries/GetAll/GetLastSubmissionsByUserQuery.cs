using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Submissions.Queries.GetAll;

public record GetLastSubmissionsByUserQuery: IRequest<List<Submission>>;

public class GetLastSubmissionsByUserQueryHandler(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<GetLastSubmissionsByUserQuery, List<Submission>>
{
    private const int MaxSubmissionToRetrieve = 3;

    public async Task<List<Submission>> Handle(GetLastSubmissionsByUserQuery request, CancellationToken cancellationToken)
	{
		var userId = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
		
		var user = await userManager.FindByIdAsync(userId!);

		return await context.Submissions.Include(s => s.Problem)
							 .Where(s => s.UserId == user!.Id)
							 .OrderByDescending(s => s.Id)
							 .Take(MaxSubmissionToRetrieve)
							 .ToListAsync(cancellationToken);
	}
}
