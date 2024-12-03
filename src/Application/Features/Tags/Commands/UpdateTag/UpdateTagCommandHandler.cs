namespace AlgoCode.Application.Features.Tags.Commands.UpdateTag;

public class UpdateTagCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTagCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        var entity = await context.Tags.FindAsync([request.Id], cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(Tag));

        bool isExist = await context.Tags.AnyAsync(t => t.Title.ToLower().Trim() == request.Title.ToLower().Trim() && t.Id != request.Id, cancellationToken);
        
        if (isExist)
        {
            validationResult.Errors.Add("Title", ["This title is already exist."]);
            return validationResult;
        }

        entity.Title = request.Title;

        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
