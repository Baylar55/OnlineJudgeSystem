namespace AlgoCode.Application.Features.Submissions.Queries.GetAll
{
	public class GetLastSubmissionsByUserQuery: IRequest<List<Submission>> { }

	public class GetLastSubmissionsByUserQueryHandler : IRequestHandler<GetLastSubmissionsByUserQuery, List<Submission>>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _context;

		public GetLastSubmissionsByUserQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_context = context;
		}

		public async Task<List<Submission>> Handle(GetLastSubmissionsByUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);

			return await _context.Submissions.Include(s => s.Problem)
								 .Where(s => s.UserId == user.Id)
								 .OrderByDescending(s => s.Id)
								 .Take(3)
								 .ToListAsync();
		}
	}
}
