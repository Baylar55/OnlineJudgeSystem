using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AlgoCode.Application.Features.Sessions.Commands.CreateSession
{
    public class CreateSessionCommand : IRequest<ValidationResultModel>
    {
        public string Title { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool IsActive { get; set; } = false;
    }

    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateSessionCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ValidationResultModel> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();

            int sessionCount = await _context.Sessions.CountAsync(x => x.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), cancellationToken);
            if (sessionCount >= 5)
            {
                validationResult.Errors.Add("Session", ["You can't create more than 5 sessions"]);
                return validationResult;
            }

            bool isExist = await _context.Sessions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);

            if (isExist)
            {
                validationResult.Errors.Add("Title", ["Session with this title already exists"]);
                return validationResult;
            }

            Session session = new()
            {
                Title = request.Title,
                IsActive = request.IsActive,
                UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
            };

            await _context.Sessions.AddAsync(session, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
