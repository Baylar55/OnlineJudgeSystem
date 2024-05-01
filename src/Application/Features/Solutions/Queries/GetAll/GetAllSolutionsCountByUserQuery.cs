using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AlgoCode.Application.Features.Solutions.Queries.GetAll
{
	public class GetAllSolutionsCountByUserQuery : IRequest<int> { }

	public class GetAllSolutionsQueryHandler : IRequestHandler<GetAllSolutionsCountByUserQuery, int>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _context;

		public GetAllSolutionsQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_context = context;
		}

		public async Task<int> Handle(GetAllSolutionsCountByUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);

			var solutions = await _context.Solutions
								 .Where(s => s.Submission.UserId == user.Id)
								 .CountAsync();
			return solutions;
		}
	}
}
