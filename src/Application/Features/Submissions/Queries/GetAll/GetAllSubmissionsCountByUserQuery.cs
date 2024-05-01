using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Submissions.Queries.GetAll
{
	public class GetAllSubmissionsCountByUserQuery: IRequest<int> { }

	public class GetAllSubmissionsCountByUserQueryHandler : IRequestHandler<GetAllSubmissionsCountByUserQuery, int>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _context;

		public GetAllSubmissionsCountByUserQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_context = context;
		}

		public async Task<int> Handle(GetAllSubmissionsCountByUserQuery request, CancellationToken cancellationToken)
		{
			var userId= _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);

			return await _context.Submissions
								 .Where(s => s.UserId == user.Id)
								 .CountAsync();
		}
	}
}
