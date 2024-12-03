namespace AlgoCode.Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTagCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();
        
        bool isExist = await context.Tags.AnyAsync(t => t.Title.ToLower().Trim() == request.Title.ToLower().Trim(), cancellationToken);
        
        if (isExist)
        {
            validationResult.Errors.Add("Title", ["This title is already exist."]);
            return validationResult;
        }
        
        var entity = request.Adapt<Tag>();

        context.Tags.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return validationResult;
    }
}
