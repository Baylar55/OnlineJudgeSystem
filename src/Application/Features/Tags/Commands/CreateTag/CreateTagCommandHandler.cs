using AlgoCode.Application.Common.Attributes;

namespace AlgoCode.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateTagCommandHandler(IApplicationDbContext context) => _context = context;

        [UniqueName]
        public async Task<ValidationResultModel> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            bool isExist = await _context.Tags.AnyAsync(t => t.Title.ToLower().Trim() == request.Title.ToLower().Trim(), cancellationToken);
            if (isExist)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Title", new List<string> { "This title is already exist." });
                return validationResult;
            }
            var entity = new Tag { Title = request.Title };

            //entity.AddDomainEvent(new TagCreatedEvent(entity));

            _context.Tags.Select(x => new Tag { });

            await _context.SaveChangesAsync(cancellationToken);

            return validationResult;

            #region Attribute
            //var validationContext = new ValidationContext(request);
            //var uniqueNameAttribute = (UniqueNameAttribute)validationContext.ObjectType
            //    .GetMember(validationContext.MemberName)[0]
            //    .GetCustomAttributes(typeof(UniqueNameAttribute), false)
            //    .FirstOrDefault();

            //if (uniqueNameAttribute != null)
            //{
            //    var validationResult = uniqueNameAttribute.GetValidationResult(request.Title, validationContext);
            //    if (validationResult != ValidationResult.Success)
            //    {
            //        return validationResult;
            //    }
            //}

            //// Create new tag
            //var entity = new Tag { Title = request.Title };
            //_context.Tags.Add(entity);
            //await _context.SaveChangesAsync(cancellationToken);

            //return ValidationResult.Success; 
            #endregion
        }
    }
}
