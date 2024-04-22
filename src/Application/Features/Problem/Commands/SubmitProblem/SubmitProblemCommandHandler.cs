namespace AlgoCode.Application.Features.Problem.Commands.SubmitProblem
{
    public class SubmitProblemCommandHandler : IRequestHandler<SubmitProblemCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public SubmitProblemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(SubmitProblemCommand request, CancellationToken cancellationToken)
        {
            var activeSession = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive, cancellationToken);
            if (activeSession == null)
                return "No active session found.";

            var submission = new Submission
            {
                SourceCode = request.SourceCode,
                Language = request.Language,
                ProblemId = request.ProblemId,
                MemoryUsage = request.MemoryUsage,
                ExecutionTime = request.ExecutionTime,
                Status = request.Status,
                UserId = request.UserId,
                SessionId = activeSession.Id
            };

            await _context.Submissions.AddAsync(submission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Status == SubmissionStatus.Accepted)
            {
                var userProblemStatus = await _context.UserProblemStatuses
                    .FirstOrDefaultAsync(u => u.UserId == request.UserId && u.ProblemId == request.ProblemId, cancellationToken);

                if (userProblemStatus != null)
                {
                    userProblemStatus.Status = ProblemStatus.Solved;
                    _context.UserProblemStatuses.Update(userProblemStatus);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    userProblemStatus = new UserProblemStatus
                    {
                        UserId = request.UserId,
                        ProblemId = request.ProblemId,
                        Status = ProblemStatus.Solved,
                        SessionId = activeSession.Id
                    };

                    await _context.UserProblemStatuses.AddAsync(userProblemStatus, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            else if (request.Status == SubmissionStatus.CompilationError)
            {
                // If submission has compilation error and there is no previous successful submission
                var previousSuccessfulSubmission = await _context.Submissions
                    .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.ProblemId == request.ProblemId
                                                && s.Status == SubmissionStatus.Accepted, cancellationToken);

                if (previousSuccessfulSubmission == null)
                {
                    // Update user problem status to attempted
                    var userProblemStatus = await _context.UserProblemStatuses
                        .FirstOrDefaultAsync(u => u.UserId == request.UserId && u.ProblemId == request.ProblemId, cancellationToken);

                    if (userProblemStatus != null)
                    {
                        userProblemStatus.Status = ProblemStatus.Attempted;
                        _context.UserProblemStatuses.Update(userProblemStatus);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }

            return submission.Id.ToString();
        }
    }

}
