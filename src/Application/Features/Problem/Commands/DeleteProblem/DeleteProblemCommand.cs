using AlgoCode.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Problem.Commands.DeleteProblem
{
    public class DeleteProblemCommand: IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
    }

    public class DeleteProblemCommandRequest:IRequestHandler<DeleteProblemCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public DeleteProblemCommandRequest(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(DeleteProblemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var problem = await _context.Problems.FindAsync(request.Id);
            if (problem == null)
                throw new NotFoundException(request.Id.ToString(), "Problem");
            
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
