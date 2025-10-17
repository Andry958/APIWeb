using BusinessLogic.DTOs.ResourceDTO;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class ResourceDTOValidator : AbstractValidator<ResourceCreateDTO>
    {
        public ResourceDTOValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.PricePerHour)
                .GreaterThan(0).WithMessage("Price per hour must be greater than 0.");

            //RuleFor(x => x.ImageUrl)
            //    .NotEmpty().WithMessage("Image URL is required.")
            //    .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            //    .WithMessage("Image URL must be a valid URL.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");
        }
    }
}
