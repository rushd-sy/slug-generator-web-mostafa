using FluentValidation;
using SlugGeneratorWeb.DTOs.Requests;

namespace SlugGeneratorWeb.Validation
{
    public class GenerateSlugValidator : AbstractValidator<GenerateSlugRequest>
    {
        public GenerateSlugValidator()
        {
            // cannot be empty, Text must be under 500 characters.
            RuleFor(slug => slug.Text).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Text should not be null.")
                .NotEmpty().WithMessage("Text should not be empty.")
                .Length(1, 500)
                .WithMessage("Text should be between 1 and 500 characters.");
            // Separator (if provided) can only be - or _.
            When(slug => slug.Separator.HasValue, () =>
            {
                RuleFor(slug => slug.Separator)
                    .Must(separator => separator == '-' || separator == '_').
                    WithMessage("Separator should be either '-' or '_'.");
            });
        }
    }
}
