namespace AlgoCode.Application.Features.Contests.Commands.CreateContest
{
    public class CreateContestCommandHandler : IRequestHandler<CreateContestCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;

        public CreateContestCommandHandler(IApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<ValidationResultModel> Handle(CreateContestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();

            if (request.StartTime > request.EndTime)
            {
                validationResult.Errors.Add("StartTime", new(["Start time must be less than end time"]));
                return validationResult;
            }

            if (request.ProblemIds == null || !request.ProblemIds.Any())
            {
                validationResult.Errors.Add("ProblemIds", new(["Contest must have at least one problem"]));
                return validationResult;
            }

            if (request.Photo == null)
            {
                validationResult.Errors.Add("Photo", new(["Photo is required"]));
                return validationResult;
            }
            bool isExist = await _context.Contests.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);
            if (isExist)
            {
                validationResult.Errors.Add(string.Empty, new(["Contest with this title already exists"]));
                return validationResult;
            }

            if (!_fileService.IsImage(request.Photo))
            {
                validationResult.Errors.Add("Photo", new(["Photo must be an image"]));
                return validationResult;
            }

            var contest = new Contest
            {
                Title = request.Title,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                IsActive = request.IsActive,
                CoverPhoto = await _fileService.UploadAsync(request.Photo)
            };

            if (request.ProblemIds != null && request.ProblemIds.Any())
                contest.Problems = await _context.Problems.Where(p => request.ProblemIds.Contains(p.Id)).ToListAsync(cancellationToken);

            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
