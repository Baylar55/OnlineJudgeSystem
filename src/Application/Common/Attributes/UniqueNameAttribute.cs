using AlgoCode.Application.Features.Tags.Commands.CreateTag;
using System.ComponentModel.DataAnnotations;

namespace AlgoCode.Application.Common.Attributes
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var dbContext = (IApplicationDbContext)validationContext.GetService(typeof(IApplicationDbContext));
            //if (dbContext == null)
            //    throw new InvalidOperationException("DbContext could not be found.");
            //var propertyName = validationContext.MemberName;
            //var tagValue = value as string;
            //var existingTag = dbContext.Tags.FirstOrDefault(t => t.Title == tagValue);
            //if (existingTag != null)
            //    return new ValidationResult("Tag name already exists.", [propertyName]);
            //return ValidationResult.Success;

            var tag = (CreateTagCommand)validationContext.ObjectInstance;
            string tagName = value.ToString();

            if (tag.Title.Trim().ToLower() == tagName.Trim().ToLower())
            {
                return new ValidationResult("Tag name already exists.", ["Tag nameeeee"]);
            }

            return ValidationResult.Success;
        }
    }

}
