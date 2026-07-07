using FluentValidation;
using ShopifyApi.DTOs.Products;

namespace ShopifyApi.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
        }
    }
}
