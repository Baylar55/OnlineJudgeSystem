namespace AlgoCode.Application.DTOs.Session;

public record ProblemCountsForSessionDTO(
    int AllProblemsCount,
    int SolvedProblemsCount,
    int AllEasyProblemsCount,
    int AllMediumProblemsCount,
    int AllHardProblemsCount,
    int SolvedEasyProblemsCount,
    int SolvedMediumProblemsCount,
    int SolvedHardProblemsCount
);