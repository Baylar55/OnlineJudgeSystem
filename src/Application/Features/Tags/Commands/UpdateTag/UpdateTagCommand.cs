namespace AlgoCode.Application.Features.Tags.Commands.UpdateTag;

public record UpdateTagCommand(int Id, string Title) : IRequest<ValidationResultModel>;
