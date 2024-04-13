﻿namespace AlgoCode.Application.Features.Problem.Commands.SubmitProblem
{
    public class SubmitProblemCommand : IRequest<string>
    {
        public string SourceCode { get; set; }
        public string Language { get; set; }
        public int ProblemId { get; set; }
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
        public SubmissionStatus Status { get; set; }
        public string UserId { get; set; }
    }

    public class SubmitProblemCommandHandler : IRequestHandler<SubmitProblemCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public SubmitProblemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(SubmitProblemCommand request, CancellationToken cancellationToken)
        {
            // Find the active session
            var activeSession = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive, cancellationToken);
            if (activeSession == null)
            {
                // Handle if no active session found
                // For example, return an error message
                return "No active session found.";
            }

            // Create a new submission
            var submission = new Submission
            {
                SourceCode = request.SourceCode,
                Language = request.Language,
                ProblemId = request.ProblemId,
                MemoryUsage = request.MemoryUsage,
                ExecutionTime = request.ExecutionTime,
                Status = request.Status,
                UserId = request.UserId,
                SessionId = activeSession.Id // Associate the submission with the active session
            };

            // Add the submission to the database
            await _context.Submissions.AddAsync(submission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // If the submission is accepted, update the user's problem status
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
                    // Create a new user problem status if not exists
                    userProblemStatus = new UserProblemStatus
                    {
                        UserId = request.UserId,
                        ProblemId = request.ProblemId,
                        Status = ProblemStatus.Solved,
                        SessionId = activeSession.Id // Associate with the active session
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