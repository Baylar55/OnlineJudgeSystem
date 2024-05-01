namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
	public class GetAllSolvedProblemsCountByUserQuery : IRequest<int> { }

	public class GetAllSolvedProblemsCountByUserQueryHandler : IRequestHandler<GetAllSolvedProblemsCountByUserQuery, int>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _context;

		public GetAllSolvedProblemsCountByUserQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_context = context;
		}

		public async Task<int> Handle(GetAllSolvedProblemsCountByUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);
			var pro= await _context.Problems
								 .Where(p => p.Submissions.Any(s => s.UserId == user.Id && s.Status == SubmissionStatus.Accepted))
								 .Distinct()
								 .CountAsync();
			return pro;
		}
	}
}
