using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Problem.Queries.GetRandom
{
    public class GetRandomProblemQuery: IRequest<int> { }

    public class GetRandomProblemQueryHandler : IRequestHandler<GetRandomProblemQuery, int>
    {
       private readonly IApplicationDbContext _context;
        public GetRandomProblemQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<int> Handle(GetRandomProblemQuery request, CancellationToken cancellationToken)
        {
            var problems = _context.Problems.ToList();
            var random = new Random();
            var index = random.Next(0, problems.Count);
            return problems[index].Id;
        }
    }
}
