namespace AlgoCode.Application.Features.Tags.Commands.CreateTag;

public record CreateTagCommand(string Title) : IRequest<ValidationResultModel>;
