using BusinessLogic.DTOs.CategoryDTO;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CategoryDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
        }
    }
}
