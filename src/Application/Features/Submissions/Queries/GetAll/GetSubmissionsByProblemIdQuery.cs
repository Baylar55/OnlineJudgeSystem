using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Submissions.Queries.GetAll
{
    public class GetSubmissionsByProblemIdQuery:IRequest<ICollection<GetSubmissionsByProblemIdQueryResponse>>
    {
        public int ProblemId { get; set; }
    }

    public class GetSubmissionsByProblemIdQueryResponse
    {
        public string Language { get; set; }
        public string Status { get; set; }
        public DateTimeOffset SubmissionTime { get; set; }
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
    }

    public class GetSubmissionsByProblemIdQueryHandler : IRequestHandler<GetSubmissionsByProblemIdQuery, ICollection<GetSubmissionsByProblemIdQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetSubmissionsByProblemIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<ICollection<GetSubmissionsByProblemIdQueryResponse>> Handle(GetSubmissionsByProblemIdQuery request, CancellationToken cancellationToken)
        {
            // get all submissions by problem id
            var submissions = await _context.Submissions.Where(x => x.ProblemId == request.ProblemId).ToListAsync();
            return submissions.Select(x => new GetSubmissionsByProblemIdQueryResponse
            {
                Language = x.Language,
                Status = x.Status.ToString(),
                SubmissionTime = x.Created,
                MemoryUsage = x.MemoryUsage,
                ExecutionTime = x.ExecutionTime
            }).ToList();
        }
    }
}
