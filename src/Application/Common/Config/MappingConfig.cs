using AlgoCode.Application.Features.Problem.Queries.GetById;
using AlgoCode.Application.Features.Solutions.Queries.GetAll;
using AlgoCode.Application.Features.Solutions.Queries.GetById;
using AlgoCode.Application.Features.Submissions.Queries.GetAll;
using AlgoCode.Application.Features.Submissions.Queries.GetById;
using AlgoCode.Application.Features.TestCases.Commands.CreateTestCase;
using AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase;
using AlgoCode.Application.Features.TestCases.Queries.GetById;

namespace AlgoCode.Application.Common.Config;

public static class MappingConfig
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<Problem, GetProblemByIdQueryResponse>.NewConfig()
            .Map(dest => dest.Difficulty, src => src.Difficulty.ToString())
            .Map(dest => dest.AccessLevel, src => src.AccessLevel.ToString())
            .Map(dest => dest.Tags, src => src.Tags.Select(tag => new Tag { Id = tag.Id, Title = tag.Title }).ToList())
            .Map(dest => dest.CreatedBy, src => src.CreatedBy != null ? src.CreatedBy : null)
            .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy != null ? src.LastModifiedBy : null);

        TypeAdapterConfig<Solution, GetAllSolutionsByProblemQueryResponse>.NewConfig()
            .Map(dest => dest.User, src => src.Submission.User);

        TypeAdapterConfig<Solution, GetSolutionByIdQueryResponse>.NewConfig()
            .Map(dest => dest.UserName, src => src.Submission.User.UserName);

        TypeAdapterConfig<Submission, GetSubmissionsByProblemIdQueryResponse>.NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString());

        TypeAdapterConfig<Submission, GetSubmissionByIdQueryResponse>.NewConfig()
            .Map(dest => dest.ProblemName, src => src.Problem.Title)
            .Map(dest => dest.Status, src => src.Status.ToString());

        TypeAdapterConfig<CreateTestCaseCommand, TestCase>.NewConfig()
            .Map(dest => dest.InputParameter, src => src.Inputs)
            .Map(dest => dest.ExpectedOutput, src => src.ExpectedOutput)
            .Map(dest => dest.ProblemId, src => src.ProblemId);

        TypeAdapterConfig<UpdateTestCaseCommand, TestCase>.NewConfig()
            .Map(dest => dest.InputParameter, src => src.Inputs)
            .Map(dest => dest.ExpectedOutput, src => src.ExpectedOutput)
            .Map(dest => dest.ProblemId, src => src.ProblemId);

        TypeAdapterConfig<TestCase, GetTestCaseByIdQueryResponse>.NewConfig()
            .Map(dest => dest.Inputs, src => src.InputParameter);

        TypeAdapterConfig<Problem, GetProblemByIdWithTestCaseQueryResponse>.NewConfig()
            .Map(dest => dest.Difficulty, src => src.Difficulty.ToString())
            .Map(dest => dest.TestCases, src => src.TestCases.Select(x => new TestCase
            {
                InputParameter = x.InputParameter,
                ExpectedOutput = x.ExpectedOutput
            }).ToList())
            .Map(dest => dest.Tags, src => src.Tags.Select(x => new Tag
            {
                Id = x.Id,
                Title = x.Title
            }).ToList());
    }
}
