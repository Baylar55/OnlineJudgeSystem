namespace AlgoCode.Application.Features.Contests.Commands.UpdateContest
{
    public class UpdateContestCommandHandler : IRequestHandler<UpdateContestCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;

        public UpdateContestCommandHandler(IApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<ValidationResultModel> Handle(UpdateContestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var contest = _context.Contests.Include(x => x.Problems).FirstOrDefault(x => x.Id == request.Id);
            if (contest == null)
                validationResult.Errors.Add("", ["Contest not found."]);

            if (request.StartTime > request.EndTime)
                validationResult.Errors.Add("StartTime", new(["Start time must be less than end time"]));

            if (await _context.Contests.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken))
                validationResult.Errors.Add("Title", new(["Contest with this title already exists"]));



            contest.Title = request.Title;
            contest.Description = request.Description;
            contest.StartTime = request.StartTime;
            contest.EndTime = request.EndTime;
            contest.IsActive = request.IsActive;

            if (request.Photo != null)
            {
                if (!_fileService.IsImage(request.Photo))
                    validationResult.Errors.Add("Photo", new(["Photo must be an image"]));
                _fileService.Delete(contest.CoverPhoto);
                contest.CoverPhoto = await _fileService.UploadAsync(request.Photo);
            }

            if (!validationResult.IsValid)
                return validationResult;

            contest.Problems.Clear();
            if (request.ProblemIds != null && request.ProblemIds.Any())
                contest.Problems = await _context.Problems.Where(p => request.ProblemIds.Contains(p.Id)).ToListAsync(cancellationToken);

            _context.Contests.Update(contest);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
