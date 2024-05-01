using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
    public class GetAllProblemsQuery : IRequest<List<Domain.Entities.Problem>> { }

    public class GetAllProblemsQueryHandler : IRequestHandler<GetAllProblemsQuery, List<Domain.Entities.Problem>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProblemsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<Domain.Entities.Problem>> Handle(GetAllProblemsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Problems.ToListAsync(cancellationToken);
        }
    }
}
