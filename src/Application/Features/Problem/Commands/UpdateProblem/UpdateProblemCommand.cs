using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Application.Features.Problem.Commands.UpdateProblem
{
    public class UpdateProblemCommand: IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProblemDifficulty Difficulty { get; set; }
        public ProblemStatus Status { get; set; }
        public int Point { get; set; }
    }
}
