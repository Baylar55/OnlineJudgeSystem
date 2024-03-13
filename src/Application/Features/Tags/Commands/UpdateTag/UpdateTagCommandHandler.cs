namespace AlgoCode.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTagCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags.FindAsync(request.Id, cancellationToken);
            var validationResult = new ValidationResultModel();
            bool isExist = await _context.Tags.AnyAsync(t => t.Title.ToLower().Trim() == request.Title.ToLower().Trim() && t.Id != request.Id, cancellationToken);
            if (isExist)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Title", new List<string> { "This title is already exist." });
                return validationResult;
            }
            entity.Title = request.Title;
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
